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
                g.FillEllipse(Brushes.Red, rectangles[i]);
                g.DrawString($"{i}", new Font("Arial", 10), Brushes.Black, new Point(rectangles[i].X + TileSize / 2, rectangles[i].Y + TileSize / 2));
            }
        }
    }

    public static class View
    {
    }
}
