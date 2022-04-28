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
        public static Dictionary<int, Color> ColorDict = new Dictionary<int, Color>()
        {
            [0] = Color.Red,
            [1] = Color.Green,
            [2] = Color.Blue,
            [3] = Color.Magenta,
            [4] = Color.Yellow,
            [5] = Color.Orange,
            [6] = Color.Pink,
            [7] = Color.Purple,
        };
        public List<Node> Nodes = new List<Node>();
        public Graph() { }
        public Graph(List<Node> list)
        {
            Nodes = list;
        }


        public void ColorGraph()
        {
            foreach (var v in Nodes)
            {
                foreach(var color in ColorDict.Keys)
                {
                    if(!v.IncindentNodes.Any(node=> node.NodeColor == ColorDict[color]))
                    {
                        v.NodeColor = ColorDict[color];
                        break;
                    }
                }
            }
        }
    }
}
