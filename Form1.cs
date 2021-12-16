using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace laba16
{
    public partial class Form1 : Form
    {
        ZedGraphControl zedGrapgControl1 = new ZedGraphControl();
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            zedGrapgControl1.Location = new Point(10, 30);
            zedGrapgControl1.Name = "text";
            zedGrapgControl1.Size = new Size(500, 500);
            Controls.Add(zedGrapgControl1);
            GraphPane my_Pane = zedGrapgControl1.GraphPane;
            my_Pane.Title.Text = "Результат";
            my_Pane.XAxis.Title.Text = "X";
            my_Pane.YAxis.Title.Text = "Y";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Clear(zedGrapgControl1);
        }
        private void Clear(ZedGraphControl Zed_GraphControl)
        {
            //GraphPane pane = Zed_GraphControl.GraphPane;
            zedGrapgControl1.GraphPane.CurveList.Clear();
            zedGrapgControl1.GraphPane.GraphObjList.Clear();

            zedGrapgControl1.GraphPane.XAxis.Type = AxisType.Linear;
            zedGrapgControl1.GraphPane.XAxis.Scale.TextLabels = null;
            zedGrapgControl1.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.RestoreScale(zedGrapgControl1.GraphPane);

            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }
        static double f1(double x, double a, double b)
        {
            return Math.Sin(x+a*b);
        }
        private void Ris(ZedGraphControl Zed_GraphControl)
        {
            GraphPane my_Pane = Zed_GraphControl.GraphPane;

            PointPairList list = new PointPairList();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            double h = 0.1, x = 0.8, a, b;

            try
            {
                a = Convert.ToDouble(textBox1.Text);
                b = Convert.ToDouble(textBox2.Text);

                double minY = 0, maxY = 0;


                while (x <= 100.0)
                {

                    list.Add(x, f1(x, a, b));
                    X.Add(x); 
                    Y.Add(f1(x, a, b));
                    x += h;
                }
                int minXpos = 0;
                int maxXpos = 0;
                for (int i = 0; i < Y.Count; i++)
                {
                    if (minY < Y[i])
                    {
                        minXpos = i;
                        minY = Y[i];
                    }
                    if (maxY > Y[i])
                    {
                        maxXpos = i;
                        maxY = Y[i];
                    }
                }
                double minX = X[minXpos];
                double maxX = X[maxXpos];

                PointPairList listMIN = new PointPairList();
                PointPairList listMAX = new PointPairList();
                for (double i = -5; i < 5; i += 0.1)
                {
                    listMIN.Add(minX, i);
                }
                for (double i = -5; i < 5; i += 0.1)
                {
                    listMAX.Add(maxX, i);
                }
                LineItem d1 = my_Pane.AddCurve("Функция", list, Color.Green, SymbolType.Circle);
                LineItem d2 = my_Pane.AddCurve("Правая граница", listMIN, Color.Orange, SymbolType.Diamond);
                LineItem d3 = my_Pane.AddCurve("Левая граница", listMAX, Color.Purple, SymbolType.Diamond);

                zedGrapgControl1.AxisChange();
                zedGrapgControl1.Invalidate();
            }
            catch
            {

                MessageBox.Show("Некорректный ввод данных");

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            sender = textBox1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            sender = textBox2;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear(zedGrapgControl1);
            Ris(zedGrapgControl1);
        }
    }
}
