namespace WinFormsAppDemoNeo4jDriver.Entities
{
    public class Movie
    {
        public Movie()
        {
            
        }

        public Movie(string title, string tagline, long? released, long? votes)
        {
            Title = title;
            Tagline = tagline;
            Released = released;
            Votes = votes;
        }

        public Movie(string title, IEnumerable<Person> cast)
        {
            Title = title;
            Cast = cast;
        }

        public string Title { get; set; }
        public IEnumerable<Person> Cast { get; set; }
        public long? Released { get; set; }
        public string Tagline { get; set; }
        public long? Votes { get; set; }
    }
}
