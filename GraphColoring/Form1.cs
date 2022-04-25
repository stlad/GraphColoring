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
            var s = str.Split(',');
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

            Controls.Add(makeGrButton);
            Controls.Add(clearButton);
            Controls.Add(addPairButton);
            Controls.Add(textBox);
            var timer = new Timer();
            timer.Interval = 15;//1000/600; 
            timer.Tick += (sender, args) =>
            {
                Invalidate();
            };
            timer.Start();


        }




    }
}
