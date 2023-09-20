using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppDemoNeo4jDriver.Entities
{
    public class Person
    {
        public Person()
        {
            
        }

        public Person(string name, string job, string role)
        {
            Name = name;
            Job = job;
            Role = role;
        }

        public string Name { get; set; }
        public string Job { get; set; }
        public string Role { get; set; }
    }
}
