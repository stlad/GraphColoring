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
        public static List<CheckBox> buttons = new List<CheckBox>();

        public static List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
        public static GraphicGraphModel GraphModel { get; set; }

        public static List<Graph> Examples = new List<Graph>();

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            int y1 = 0;
            foreach (var p in pairs)
            {
                g.DrawString($"({p.Item1} {p.Item2})\n", new Font("Arial", 20), Brushes.Black, new PointF(50, 100 + y1 * 30));
                y1++;
            }

            //var r = new Rectangle(650, 300, 350, 350);
            //g.DrawRectangle(Pens.Black, r);
            //var r1 = new Rectangle(650, 300, 40, 40);
            //g.DrawRectangle(Pens.Black, r1);

            GraphModel.DrawConections(g);
            GraphModel.DrawGraph(g);

        }


        public void AddPair(TextBox textBox)
        {
            var str = textBox.Text;
            var s = str.Split(',','.');
            if(s.Length!=2)
            {
                textBox.Text = "Неверный ввод!";
                return;
            }

            int num1;
            int num2;
            if(!int.TryParse(s[0], out num1) || !int.TryParse(s[1], out num2))
            {
                textBox.Text = "Неверный ввод!";
                return;
            }

            if(num1 >=8 || num2 >=8 || num1<0 || num2<0)
            {
                textBox.Text = "Неверный ввод!";
                return;
            }

            if(num1==num2)
            {
                textBox.Text = "Петли не корректны!";
                return;
            }
            pairs.Add(Tuple.Create(num1, num2));
            textBox.Text = "Успешно!";
        }


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ClientSize = new Size(1000, 700);
            GraphModel = new GraphicGraphModel(new Graph());
            this.BackColor = Color.LightGray;



            var textBox = new TextBox()
            {
                MinimumSize = new Size(200, 30),
                Left = 50,
                Top = 50,
                Text = "Введите пару узлов через пробел"

            };
            textBox.Click += (s, a) => textBox.Text = "";

            var addPairButton = new Button()
            { 
                Text = "Добавить пару",
                Left = textBox.Right + 20,
                Top = textBox.Top,
                Size = new Size(100, textBox.Height)
            };
            addPairButton.Click += (semd, args) => AddPair(textBox);

            var clearButton = new Button()
            {
                Text = "Очистить",
                Left = textBox.Right + 20,
                Top = addPairButton.Bottom,
                Size = new Size(100, textBox.Height)
            };
            clearButton.Click += (s, a) =>
            {
                pairs.Clear();
                GraphModel = new GraphicGraphModel(new Graph());
            };

            var makeGrButton = new Button()
            {
                Text = "Создать граф",
                Left = textBox.Right + 20,
                Top = clearButton.Bottom,
                Size = new Size(100, textBox.Height)
            };
            makeGrButton.Click += (s, a) =>
             {
                 var graph = GraphFactory.GetGraph(pairs);
                 GraphModel = new GraphicGraphModel(graph);
             };


            var colorGraphButton = new Button()
            {
                Text = "Раскрасить граф",
                Left = textBox.Right + 20,
                Top = makeGrButton.Bottom + 100,
                Size = new Size(100, textBox.Height+50)
            };
            colorGraphButton.Click += (s, a) => GraphModel.Graph.ColorGraph();

            Controls.Add(colorGraphButton);
            Controls.Add(makeGrButton);
            Controls.Add(clearButton);
            Controls.Add(addPairButton);
            Controls.Add(textBox);
            GetGraphExamples();


            var timer = new Timer();
            timer.Interval = 15;//1000/600; 
            timer.Tick += (sender, args) =>
            {
                Invalidate();
            };
            timer.Start();


        }


        public void MakeExamplesButton()
        {
            //int dx = 50;
            for(int i = 0; i <Examples.Count;i++)
            {
                var g = Examples[i];
                var butt = new Button()
                {
                    Text = $"Пример {i + 1}",
                    Left = 50 + 100 * i,
                    Top = ClientRectangle.Bottom - 100,
                    Size = new Size(100, 30)
                };
                butt.Click += (s, a) => GraphModel = new GraphicGraphModel(g);
                Controls.Add(butt);
            }


           


        }

        public void  GetGraphExamples()
        {
            var g1 = new List<Tuple<int,int>>()
            {
                Tuple.Create(5,4),
                Tuple.Create(3,4),
                Tuple.Create(5,3),
                Tuple.Create(5,0),
            };
            Examples.Add(GraphFactory.GetGraph(g1));

            var g2 = new List<Tuple<int, int>>()
            {
                Tuple.Create(0,1),
                Tuple.Create(0,2),
                Tuple.Create(0,3),
                Tuple.Create(0,4),
                Tuple.Create(0,5),
                Tuple.Create(0,6),
                Tuple.Create(0,7),
            };
            Examples.Add(GraphFactory.GetGraph(g2));



            var g3 = new List<Tuple<int, int>>();
            for(int i = 0; i < 8;i++)
            {
                for(int j = 0; j < 8;j++)
                {
                    if (i == j) continue;
                    g3.Add(Tuple.Create(i, j));
                }
            }
            Examples.Add(GraphFactory.GetGraph(g3));

            var g4 = new List<Tuple<int, int>>()
            {
                Tuple.Create(1, 6),
                Tuple.Create(4, 6),
                Tuple.Create(1, 4),
                Tuple.Create(2, 6),
                Tuple.Create(3, 6),
                Tuple.Create(2, 3),
            };
            Examples.Add(GraphFactory.GetGraph(g4));


            MakeExamplesButton();
        }

    }
}
