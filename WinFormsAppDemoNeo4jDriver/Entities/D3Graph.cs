using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace WinFormsAppDemoNeo4jDriver.Entities
{
    public class D3Graph
    {
        public D3Graph(IEnumerable<D3Node> nodes, IEnumerable<D3Link> links) { }
        public IEnumerable<D3Node> Nodes { get; set; }
        public IEnumerable<D3Link> Links { get; set; }
    }

    public class D3Node
    {
        public D3Node(string title, string lable)
        {
            Title = title;
            Label = lable;
        }
        public string Title { get; set; }
        public string Label { get; set; }
    }

    public class D3Link
    {
        public D3Link(int source, int target)
        {
            Source = source;
            Target = target;
        }
        public int Source { get; set; }
        public int Target { get; set; }
    }
}
