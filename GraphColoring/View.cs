using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    public class GraphicGraphModel
    {
        public Graph Graph { get; set; }
        public Rectangle[] rectangles { get; set; }

        public Point Center = new Point(650, 300);

        public int WholeSize = 400;
        public int TileSize = 40;

        public GraphicGraphModel(Graph g)
        {
            Graph = g;
            rectangles = new Rectangle[8];
            rectangles[0] = new Rectangle(Center.X - TileSize / 2, Center.Y - WholeSize/2, TileSize, TileSize);
            rectangles[2] = new Rectangle(Center.X + (WholeSize/2)-TileSize, Center.Y - TileSize/2, TileSize, TileSize);
            rectangles[4] = new Rectangle(Center.X - TileSize / 2, Center.Y + WholeSize/2-TileSize, TileSize, TileSize);
            rectangles[6] = new Rectangle(Center.X - WholeSize / 2, Center.Y - TileSize/2, TileSize, TileSize);


            rectangles[1] = new Rectangle(Center.X + WholeSize/5 + TileSize / 2, Center.Y-WholeSize/4-TileSize/2, TileSize, TileSize);
            rectangles[3] = new Rectangle(Center.X + WholeSize / 5 + TileSize / 2, Center.Y + WholeSize / 4 - TileSize / 2, TileSize, TileSize);
            rectangles[5] = new Rectangle(Center.X - WholeSize / 5 - TileSize*3/2, Center.Y + WholeSize / 4 - TileSize / 2, TileSize, TileSize);
            rectangles[7] = new Rectangle(Center.X - WholeSize / 5 - TileSize*3/2, Center.Y - WholeSize / 4 - TileSize / 2, TileSize, TileSize);
        }

        public void DrawGraph(Graphics g)
        {
            for(int i = 0; i < rectangles.Length; i++)
            {
                var nodeIndex = Graph.Nodes.Where(n => n.NodeIndex == i).ToList();
                var currentCol = nodeIndex.Count == 1 ? nodeIndex[0].NodeColor : Color.LightGray;
                g.FillEllipse(new SolidBrush(currentCol), rectangles[i]);
                g.DrawString($"{i}", new Font("Arial", 10), Brushes.Black, new Point(rectangles[i].X + TileSize / 2, rectangles[i].Y + TileSize / 2));
            }
        }

        public void DrawConections(Graphics g)
        {
            foreach(var node in Graph.Nodes)
            {
                var index = node.NodeIndex;
                foreach(var secNode in node.IncindentNodes)
                {
                    var secIndex = secNode.NodeIndex;

                    var fpt = new Point(rectangles[index].X + TileSize/2 , rectangles[index].Y + TileSize / 2);
                    var spt = new Point(rectangles[secIndex].X + TileSize / 2, rectangles[secIndex].Y + TileSize / 2);

                    g.DrawLine(new Pen(Color.Black,2), fpt, spt);
                }
            }
        }
    }

    public static class View
    {
    }
}
