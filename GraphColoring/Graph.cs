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

            var fin = allNodes.Where(x=> x.IncindentNodes!=null).ToList();
            return new Graph(fin);
        }
    }

    public class Node
    {
        public List<Node> IncindentNodes { get; set; }
        public Color NodeColor { get; set; }

        public int NodeIndex { get; set; }
    }

    public class Graph
    {
        public List<Node> Nodes { get; set; }
        public Graph() { }
        public Graph(List<Node> list)
        {
            Nodes = list;
        }

    }
}
