using Microsoft.VisualBasic.Devices;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsAppDemoNeo4jDriver.Entities;

namespace WinFormsAppDemoNeo4jDriver.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return DataProvider.instance;
            }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        private string _uri = "bolt://localhost:7687";
        private string _username = "neo4j";
        private string _password = "12345678";
        private string _database = "neo4j";

        public async Task<List<Movie>> Search(string search)
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            return await session.ExecuteReadAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(@"
                        MATCH (movie:Movie)
                        WHERE toLower(movie.title) CONTAINS toLower($title)
                        RETURN movie.title AS title,
                               movie.released AS released,
                               movie.tagline AS tagline,
                               movie.votes AS votes",
                    new { title = search }
                );

                return await cursor.ToListAsync(record => new Movie(
                    record["title"].As<string>(),
                    record["tagline"].As<string>(),
                    record["released"].As<long>(),
                    record["votes"]?.As<long>()
                ));
            });
        }

        public async Task<Movie> FindByTitle(string title)
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            return await session.ExecuteReadAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(@"
                        MATCH (movie:Movie {title:$title})
                        OPTIONAL MATCH (movie)<-[r]-(person:Person)
                        RETURN movie.title AS title,
                               collect({
                                   name:person.name,
                                   job: head(split(toLower(type(r)),'_')),
                                   role: reduce(acc = '', role IN r.roles | acc + CASE WHEN acc='' THEN '' ELSE ', ' END + role)}
                               ) AS cast",
                    new { title }
                );

                return await cursor.SingleAsync(record => new Movie(
                    record["title"].As<string>(),
                    MapCast(record["cast"].As<List<IDictionary<string, object>>>())
                ));
            });
        }

        public async Task<int> VoteByTitle(string title)
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            return await session.ExecuteWriteAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(@"
                            MATCH (m:Movie {title: $title})
                            SET m.votes = coalesce(m.votes, 0) + 1;",
                    new { title }
                );

                var summary = await cursor.ConsumeAsync();
                return summary.Counters.PropertiesSet;
            });
        }

        public async Task<List<Person>> GetPersons()
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            return await session.ExecuteReadAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(@"
                        MATCH (p:Person)
                        RETURN p.name AS name"
                );

                return await cursor.ToListAsync(record => new Person(record["name"].As<string>(), "", ""
                ));
            });
        }

        public async Task AddMovie(Movie movie)
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            await session.ExecuteWriteAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync("CREATE (m: Movie " + "{ title: '" + movie.Title + "', tagline: '" + movie.Tagline + "', released: " + movie.Released + ", votes: " + movie.Votes + "})"
                );
            });

            foreach(Person p in movie.Cast)
            {
                await session.ExecuteWriteAsync(async transaction =>
                {
                    await transaction.RunAsync("MATCH (p: Person {name: '" + p.Name + "'}) MATCH (m: Movie {title: '" + movie.Title + "'}) CREATE (p)-[rel:" + p.Role + "]->(m)"
                    );
                });
            }
        }

        public async Task DeleteMovie(string title)
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            await session.ExecuteWriteAsync(async transaction =>
            {
                await transaction.RunAsync("MATCH (m: Movie {title: '" + title + "'}) DETACH DELETE m"
                );
            });
        }

        public async Task<D3Graph> FetchD3Graph(int limit)
        {
            var driver = GraphDatabase.Driver(_uri, AuthTokens.Basic(_username, _password));
            await using var session = driver.AsyncSession(o => o.WithDatabase(_database));

            return await session.ExecuteReadAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(@"
                        MATCH (m:Movie)<-[:ACTED_IN]-(p:Person)
                        WITH m, p
                        ORDER BY m.title, p.name
                        RETURN m.title AS title, collect(p.name) AS cast
                        LIMIT $limit",
                    new { limit }
                );

                var nodes = new List<D3Node>();
                var links = new List<D3Link>();

                // IAsyncEnumerable available from Version 5.5 of .NET Driver.
                await foreach (var record in cursor)
                {
                    var movie = new D3Node(record["title"].As<string>(), "movie");
                    var movieIndex = nodes.Count;
                    nodes.Add(movie);
                    foreach (var actorName in record["cast"].As<IList<string>>())
                    {
                        var actor = new D3Node(actorName, "actor");
                        var actorIndex = nodes.IndexOf(actor);
                        actorIndex = actorIndex == -1 ? nodes.Count : actorIndex;
                        nodes.Add(actor);
                        links.Add(new D3Link(actorIndex, movieIndex));
                    }
                }

                return new D3Graph(nodes, links);
            });
        }



        private static IEnumerable<Person> MapCast(IEnumerable<IDictionary<string, object>> persons)
        {
            return persons
                .Select(dictionary =>
                    new Person(
                        dictionary["name"].As<string>(),
                        dictionary["job"].As<string>(),
                        dictionary["role"].As<string>()
                    )
                ).ToList();
        }

    }
}
