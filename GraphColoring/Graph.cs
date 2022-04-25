using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    public static class GraphFactory
    {
        public static Graph GetGraph(List<Tuple<int, int>> pairs)
        {
            var allNodes = new List<Node>();

            for(int i = 0; i< 8;i++)
            {
                var node = new Node();
                node.NodeIndex = i;
                allNodes.Add(node);
            }
                

            foreach(var pair in pairs)
            {
                var f = pair.Item1;
                var s = pair.Item2;

                allNodes[f].IncindentNodes.Add(allNodes[s]);
                allNodes[s].IncindentNodes.Add(allNodes[f]);
            }

            var fin = allNodes.Where(x=> x.IncindentNodes.Count!=0).ToList();
            return new Graph(fin);
        }
    }

    public class Node
    {
        public List<Node> IncindentNodes = new List<Node>();
        public Color NodeColor = Color.DarkGray;
        public int NodeIndex { get; set; }

    }

    public class Graph
    {
        public List<Node> Nodes = new List<Node>();
        public Graph() { }
        public Graph(List<Node> list)
        {
            Nodes = list;
        }

    }
}
