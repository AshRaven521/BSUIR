using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr2
{
    public class GraphNode
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }
        public GraphNode(int source, string color, int dest = 0, int weight = 0)
        {
            Source = source;
            Destination = dest;
            Weight = weight;
            Color = color;
        }

    }
}
