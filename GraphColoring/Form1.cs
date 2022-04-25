using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphColoring
{
    
    public partial class Form1 : Form
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            //var buffer = buttons.Where(x => x.Checked).ToList();
            //if (buffer.Count == 2)
            //{
            //    pairs.Add(Tuple.Create(buffer[0].Name, buffer[1].Name));
            //    buffer[0].Checked = false;
            //    buffer[1].Checked = false;
            //    buffer.Clear();
            //}

            int y1 = 0;
            g.DrawString($"({pairs.Count})", new Font("Arial",20), Brushes.Black, new PointF(600, 300 + y1 * 30));
            foreach(var p in pairs)
            {
                //g.DrawString($"({p.Name})\n", new Font("Arial", 20), Brushes.Black, new PointF(600, 300 + y1 * 30));
                y1++;
            }
            int y = 0;
            foreach (var p in buttons)
            { 
                g.DrawString($"({p.Name})\n", new Font("Arial", 20), Brushes.Black, new PointF(500,300+y*30));
                y++;
            }
            
        }

        public static List<CheckBox> buttons = new List<CheckBox>();

        public static List<Tuple<string, string>> pairs = new List<Tuple<string, string>>();
        public static List<CheckBox> buffer = new List<CheckBox>();
        public static void CreateGraph()
        {
            int index = 0;
            for (int i = 0; i< 3; i++)
            {
                for(int j =0;j<4; j++)
                {
                    var button = new CheckBox()
                    {
                        Checked = false,
                        BackColor = Color.Red,
                        Left = 40 + 150 * i,
                        Top = 40 + 150 * j,
                        Size = new Size(40, 40),
                        Name = Convert.ToString(index),
                        Text = Convert.ToString(index)
                    };
                    buttons.Add(button);
                    button.CheckedChanged += (s, arg) =>
                     {
                         if (button.Checked)
                         {
                             button.BackColor = Color.Green;
                             buffer.Add(button);
                             if (buffer.Count == 2)
                             {
                                 pairs.Add(Tuple.Create(buffer[0].Name, buffer[1].Name));
                             }
                             else if (buffer.Count == 3)
                             {
                                 buffer[0].Checked = false;
                                 pairs.Add(Tuple.Create(buffer[0].Name, buffer[1].Name));
                             }
                         }
                         else
                         {
                             if(buffer.Contains(button))
                                buffer.Remove(button);
                             button.BackColor = Color.Red;
                         }
                     };
                    index++;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(500, 700);
            CreateGraph();

            foreach(var butt in buttons)
            {
                Controls.Add(butt);
            }

            //var timer = new Timer();
            //timer.Interval = 15;//1000/600; 
            //timer.Tick += (sender, args) =>
            //{
            //    Invalidate();
            //};
            //timer.Start();
        }




    }
}
