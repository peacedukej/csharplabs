using System;
using System.Drawing;
using System.Windows.Forms;
namespace mp_lab3_kiryanov
{
    public partial class Form1 : Form
    {
        Graphics gr;
        TextBox tb;
        // одно деление на графике в пикселях
        public double delenie = 30;
        int N = 3;

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
        }
        private void SetupDataGridView()
        {
            this.Controls.Add(dataGridView1);

            dataGridView1.ColumnCount = N;
            dataGridView1.RowCount = 3;

            dataGridView1.Name = "dataGridView1";
            dataGridView1.GridColor = Color.Black;
            dataGridView1.RowHeadersVisible = true;

            for (int i = 0; i < N; i++) {
                dataGridView1[i, 0].Value = i;
                dataGridView1[i, 1].Value = i;
                dataGridView1[i, 2].Value = i;
            }

            
            if (N == 24) {
                // Правая дуга
                    dataGridView1[0, 1].Value = 4;
                    dataGridView1[0, 2].Value = -2;

                    dataGridView1[1, 1].Value = 1;
                    dataGridView1[1, 2].Value = 0;

                    dataGridView1[2, 1].Value = 4;
                    dataGridView1[2, 2].Value = 2;

                // Верхняя дуга

                    dataGridView1[3, 1].Value = -2;
                    dataGridView1[3, 2].Value = -4;

                    dataGridView1[4, 1].Value = 0;
                    dataGridView1[4, 2].Value = -1;

                    dataGridView1[5, 1].Value = 2;
                    dataGridView1[5, 2].Value = -4;

                // Нижняя дуга

                    dataGridView1[6, 1].Value = 2;
                    dataGridView1[6, 2].Value = 4;

                    dataGridView1[7, 1].Value = 0;
                    dataGridView1[7, 2].Value = 1;

                    dataGridView1[8, 1].Value = -2;
                    dataGridView1[8, 2].Value = 4;

                // Левая дуга
                    dataGridView1[9, 1].Value = -4;
                    dataGridView1[9, 2].Value = 2;

                    dataGridView1[10, 1].Value = -1;
                    dataGridView1[10, 2].Value = 0;

                    dataGridView1[11, 1].Value = -4;
                    dataGridView1[11, 2].Value = -2;

                // Линия право верх
                    dataGridView1[12, 1].Value = 4;
                    dataGridView1[12, 2].Value = -2;

                    dataGridView1[13, 1].Value = 3;
                    dataGridView1[13, 2].Value = -3;

                    dataGridView1[14, 1].Value = 2;
                    dataGridView1[14, 2].Value = -4;

                // Линия лево верх
                    dataGridView1[15, 1].Value = -4;
                    dataGridView1[15, 2].Value = -2;

                    dataGridView1[16, 1].Value = -3;
                    dataGridView1[16, 2].Value = -3;

                    dataGridView1[17, 1].Value = -2;
                    dataGridView1[17, 2].Value = -4;

                // Линия лево низ
                    dataGridView1[18, 1].Value = -4;
                    dataGridView1[18, 2].Value = 2;

                    dataGridView1[19, 1].Value = -3;
                    dataGridView1[19, 2].Value = 3;

                    dataGridView1[20, 1].Value = -2;
                    dataGridView1[20, 2].Value = 4;

                // Линия право низ
                    dataGridView1[21, 1].Value = 2;
                    dataGridView1[21, 2].Value = 4;

                    dataGridView1[22, 1].Value = 3;
                    dataGridView1[22, 2].Value = 3;

                    dataGridView1[23, 1].Value = 4;
                    dataGridView1[23, 2].Value = 2;
            }
            dataGridView1.Rows[0].HeaderCell.Value = "Point";
            dataGridView1.Rows[1].HeaderCell.Value = "X";
            dataGridView1.Rows[2].HeaderCell.Value = "Y";
            dataGridView1.Rows[0].ReadOnly = true;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
                e.Graphics.TranslateTransform(w / 2, h / 2);
                DrawXAxis(new System.Drawing.Point(-w / 2, 0), new System.Drawing.Point(w / 2, 0), e.Graphics);
                DrawYAxis(new System.Drawing.Point(0, h / 2), new System.Drawing.Point(0, -h / 2), e.Graphics);
                DrawFunct(new System.Drawing.Point(-w / 2, h / 2), new System.Drawing.Point(w / 2, h / 2), w, e.Graphics);
        }

        private void DrawFunct(Point start, Point end, int w, Graphics gr)
        {
            
            Point[] basic_curve = new Point[N];
            
                for (int i = 0; i < N; i++)
                    basic_curve[i] = new
                        Point(Convert.ToInt32(dataGridView1[i, 1].Value) * (int)delenie,
                        Convert.ToInt32(dataGridView1[i, 2].Value) * (int)(delenie));

            switch (N) {
                case 3:
                    first_Bezier(gr, basic_curve);
                    break;
                case 4:
                    second_Bezier(gr, basic_curve);
                    break;
                case 7:
                    third_Bezier(gr, basic_curve);
                    break;
                case 24:
                    // фигура из моего задания

                    Point[] curve = new Point[3];
                    Array.Copy(basic_curve, 0, curve, 0, 3);
                    third_Bezier(gr, curve);
                    
                    Array.Copy(basic_curve, 3, curve, 0, 3);
                    third_Bezier(gr, curve);
                    
                    Array.Copy(basic_curve, 6, curve, 0, 3);
                    third_Bezier(gr, curve);

                    Array.Copy(basic_curve, 9, curve, 0, 3);
                    third_Bezier(gr, curve);

                    Point[] line = new Point[3];

                    Array.Copy(basic_curve, 12, line, 0, 3);
                    first_Bezier(gr, line);

                    Array.Copy(basic_curve, 15, line, 0, 3);
                    first_Bezier(gr, line);

                    Array.Copy(basic_curve, 18, line, 0, 3);
                    first_Bezier(gr, line);

                    Array.Copy(basic_curve, 21, line, 0, 3);
                    first_Bezier(gr, line);
                    break;
            }
        }

        // полинома Бернштейна
        float polinom(int i, int n, float t)
        {
            return (Fuctorial(n) / (Fuctorial(i) * Fuctorial(n - i))) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }
        // вычисление факториала
        int Fuctorial(int n) 
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }

        void first_Bezier(Graphics gr, Point[] Arr)
        {
            if (Arr[0].X > Arr[2].X) { Point swap = Arr[0]; Arr[0] = Arr[2]; Arr[2] = swap; }
            double Step = 1.0 / (double)(Arr[2].X - Arr[0].X);
            Point[] points = new Point[2];
            double t = 0;
            points[0] = new Point(Arr[0].X, Arr[0].Y);
            for (int i = Arr[0].X; i < Arr[2].X; i++)
            {
                t = (double)(i - Arr[0].X) * Step;
                double q1 = Math.Pow(1 - t, 2);
                double q2 = 2 * t * (1 - t);
                double q3 = t * t;
                double qx = q1 * Arr[0].X + q2 * Arr[1].X + q3 * Arr[2].X;
                double qy = q1 * Arr[0].Y + q2 * Arr[1].Y + q3 * Arr[2].Y;
                points[1].X = (int)qx;
                points[1].Y = (int)qy;
                Pen MyPen = new Pen(Color.Black, 3);
                gr.DrawLines(MyPen, points);
                points[0] = points[1];
            }
        }

        void second_Bezier(Graphics gr, Point[] Arr)
        {
            double Step = 1.034 / (double)(Arr[3].X - Arr[0].X);
            Point[] points = new Point[2];
            double t = 0;
            points[0] = new Point(Arr[0].X, Arr[0].Y);
            for (int i = Arr[0].X; i < Arr[3].X; i++)
            {
                t = (double)(i - Arr[0].X) * Step;
                double q1 = t * t * t * (-1) + t * t * 3 + t * (-3) + 1;
                double q2 = t * t * t * 3 + t * t * (-6) + t * 3;
                double q3 = t * t * t * (-3) + t * t * 3;
                double q4 = t * t * t;
                double qx = q1 * Arr[0].X + q2 * Arr[1].X + q3 * Arr[2].X + q4 * Arr[3].X;
                double qy = q1 * Arr[0].Y + q2 * Arr[1].Y + q3 * Arr[2].Y + q4 * Arr[3].Y;
                points[1].X = (int)qx;
                points[1].Y = (int)qy;
                Pen MyPen = new Pen(Color.Black, 3);
                gr.DrawLines(MyPen, points);
                points[0] = points[1];
            }
        }


        void third_Bezier(Graphics gr, Point[] Arr)
        {
            int j = 0; float step = 0.01f;
            Point[] points = new Point[101];
            for (float t = 0; t < 1; t += step) {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < Arr.GetLength(0); i++) { 
                    float b = polinom(i, Arr.GetLength(0) - 1, t);
                    xtmp += Arr[i].X * b; 
                    ytmp += Arr[i].Y * b;
                }
                points[j] = new Point((int)xtmp, (int)ytmp);
                j++;
            }
            Pen MyPen = new Pen(Color.Black, 3);
            gr.DrawLines(MyPen, points);
        }



        // ось X
        private void DrawXAxis(System.Drawing.Point start, System.Drawing.Point end, Graphics gr)
        {

            // ось и значения
            gr.DrawLine(Pens.Black, start, end);
            DrawText(new System.Drawing.Point(end.X * 98 / 100, end.Y), "X", gr, false);

            // положительные деления
            for (double i = delenie; i < end.X; i += delenie)
                {
                    gr.DrawLine(Pens.Black, (int)i, -3, (int)i, 3);
                    DrawText(new System.Drawing.Point((int)i, 3), (i / delenie).ToString(), gr);
                }

            // отрицательные деления
            for (double i = -delenie; i > start.X; i -= delenie)
            {
                gr.DrawLine(Pens.Black, (int)i, -3, (int)i, 3);
                DrawText(new System.Drawing.Point((int)i, 3), (i / delenie).ToString(), gr);
            }
            
        }

        // ось Y
        private void DrawYAxis(System.Drawing.Point start, System.Drawing.Point end, Graphics gr)
        {
            // ось и значения
            gr.DrawLine(Pens.Black, start, end);

            DrawText(new System.Drawing.Point(end.X * 110 / 100, end.Y * 98 / 100), " Y", gr, true);

            // положительные деления 
            for (double i = delenie; i < start.Y; i += delenie)
                {
                    gr.DrawLine(Pens.Black, -3, (int)i, 3, (int)i);
                    DrawText(new System.Drawing.Point(3, (int)i), (-i / delenie).ToString(), gr, true);
                }

            // отрицательные деления
            for (double i = -delenie; i > end.Y; i -= delenie)
            {
                gr.DrawLine(Pens.Black, -3, (int)i, 3, (int)i);
                DrawText(new System.Drawing.Point(3, (int)i), (-i / delenie).ToString(), gr, true);
            }
        }

        // цифры делений на графике и символы
        private void DrawText(System.Drawing.Point Point, string text, Graphics gr, bool isYAxis = false)
        {
            Font f = new Font(System.Drawing.FontFamily.GenericMonospace, 9.0F,
                                System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel);
            var size = gr.MeasureString(text, f);
            var pt = isYAxis
                ? new PointF(Point.X + 1, Point.Y - size.Height / 2)
                : new PointF(Point.X - size.Width / 2, Point.Y + 1);
            var rect = new RectangleF(pt, size);
            gr.DrawString(text, f, Brushes.Black, rect);
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Index) {
                case 0:
                    N = 3;
                    SetupDataGridView();
                    dataGridView1.Show();
                    break;
                case 1:
                    N = 4;
                    SetupDataGridView();
                    dataGridView1.Show();
                    break;
                case 2:
                    N = 7;
                    SetupDataGridView();
                    dataGridView1.Show();
                    break;
                case 3:
                    N = 24;
                    SetupDataGridView();
                    dataGridView1.Hide();
                    break;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void DG_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(Char.IsDigit(e.KeyChar)))
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;

        }


        private void dataGridView1_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(DG_KeyPress);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}