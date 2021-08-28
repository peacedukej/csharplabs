using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace lab5_mp_kiryanov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const double from = 9.5;
        const double to = 11.5;
        const double step = 0.05;
        public void Quad_form(ref System.Windows.Forms.DataVisualization.Charting.Chart Chart,
                                                                Dictionary<double, double> coords)
        {
            double y;
            double[,] a = new double[3, 3];
            double[] b = new double[3];
            double[] _x = new double[3];

            for (int i = 0; i < 3; i++)
                _x[i] = 0;

            foreach (var pair in coords) {
                Console.WriteLine("Key: " + pair.Key + " Value: " + pair.Value + '\n');
            }

            a[0, 0] = coords.Count;
            a[0, 1] = Coeffs_for_quad(coords, 0, 1);
            a[0, 2] = Coeffs_for_quad(coords, 0, 2);
            a[1, 0] = Coeffs_for_quad(coords, 0, 1);
            a[1, 1] = Coeffs_for_quad(coords, 0, 2);
            a[1, 2] = Coeffs_for_quad(coords, 0, 3);
            a[2, 0] = Coeffs_for_quad(coords, 0, 2);
            a[2, 1] = Coeffs_for_quad(coords, 0, 3);
            a[2, 2] = Coeffs_for_quad(coords, 0, 4);
            b[0] = Coeffs_for_quad(coords, 1, 1);
            b[1] = Coeffs_for_quad(coords, 2, 1);
            b[2] = Coeffs_for_quad(coords, 2, -2);
            Solve_system(3, a, b, ref _x);

            for (double i = from ;i < to; i += step)           
            {
                y = _x[2] * Math.Pow(i, 2) + _x[1] * i + _x[0];
                Chart.Series["Square"].Points.AddXY(i, y);
            }
            Chart.Series["Square"].Color = Color.Red;
            label1.Text += Math.Round(_x[0], 3) + " + " + Math.Round(_x[1], 3) + "x" + " + " + Math.Round(_x[2], 3) + "x^2";
            label2.Text += _x[1];
        }

        public double Coeffs_for_quad(Dictionary<double, double> coordinates, int value, int pow)
        {
            double result = 0;

            if (value == 1)
                foreach (var pair in coordinates)
                    result += Math.Pow(pair.Value, pow);
            else if (value == 0)
                foreach (var pair in coordinates)
                    result += Math.Pow(pair.Key, pow);
            else
            {
                foreach (var pair in coordinates)
                {
                    if (pow < 0)
                        result += pair.Value * Math.Pow(pair.Key, (-pow));
                    else
                        result += Math.Pow(pair.Value, pow) * Math.Pow(pair.Key, pow);
                }
            }
            //Console.WriteLine("result " + result + '\n');
            return result;
        }


        public void Solve_system(int size, double[,] a, double[] b, ref double[] x)
        {
            double s;
            int n = size;

            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    for (int j = k + 1; j < n; j++)
                    {
                        a[i, j] = a[i, j] - a[k, j] * (a[i, k] / a[k, k]);
                    }
                    b[i] = b[i] - b[k] * a[i, k] / a[k, k];
                }
            }
            for (int k = n - 1; k >= 0; k--)
            {
                s = 0;
                for (int j = k + 1; j < n; j++)
                    s += a[k, j] * x[j];
                x[k] = (b[k] - s) / a[k, k];
               //Console.WriteLine(x[k] + '\n');
            }
        }


        public void Hyperbolic_Form(ref System.Windows.Forms.DataVisualization.Charting.Chart Chart,
                                                                Dictionary<double, double> coordinates)
        {
            double y = 0;
            double[,] a = new double[2, 2];
            double[] b = new double[2];
            double[] _x = new double[2];

            for (int i = 0; i < 2; i++)
                _x[i] = 0;
            a[0, 0] = coordinates.Count;
            a[0, 1] = Coeffs_for_hyperbolic(coordinates, 0, 1);
            a[1, 0] = Coeffs_for_hyperbolic(coordinates, 0, 1);
            a[1, 1] = Coeffs_for_hyperbolic(coordinates, 0, 2);
            b[0] = Coeffs_for_quad(coordinates, 1, 1);
            b[1] = Coeffs_for_hyperbolic(coordinates, 1, 1);
            Solve_system(2, a, b, ref _x);
            for (double i = from; i < to; i += step)
            {
                y = _x[0] + _x[1] / i;
                Chart.Series["Hyper"].Points.AddXY(i, y);
            }
            Chart.Series["Hyper"].Color = Color.Blue;
            label4.Text += Math.Round(_x[0], 3) + " + " + Math.Round(_x[1], 3) + " / x";
            label5.Text += _x[1];
        }


        public double Coeffs_for_hyperbolic(Dictionary<double, double> coordinates, int value, int pow)
        {
            double result = 0;
            if (value == 0)
                foreach (var pair in coordinates)
                    result += 1 / Math.Pow(pair.Key, pow);
            else
                foreach (var pair in coordinates)
                    result += Math.Pow(pair.Value, pow) / Math.Pow(pair.Key, pow);
            return result;
        }


        // парсинг строк
        public void Get_values_from_string_and_put_to_dataGrid(string line, int row)
        {
            double[] numbers = line.Split(' ').Select(snum => double.Parse(snum)).Take(6).ToArray();
            for (int i = 1; i < 7; i++)
            {
                dataGridView1.Rows[row].Cells[i].Value = numbers[i-1];
                dataGridView1.Columns[i].Width = 60;
                dataGridView1.Columns[0].Width = 50;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // чтение из файла

            string path = Application.StartupPath + "\\data.txt";

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string first_line, second_line;
                first_line = sr.ReadLine();
                second_line = sr.ReadLine();

                dataGridView1.ColumnCount = 7;
                dataGridView1.RowCount = 2;
                dataGridView1.Rows[0].Cells[0].Value = "X";
                dataGridView1.Rows[1].Cells[0].Value = "Y";
                Get_values_from_string_and_put_to_dataGrid(first_line, 0);
                Get_values_from_string_and_put_to_dataGrid(second_line, 1);
            }

            Dictionary<double, double> coordinates = new Dictionary<double, double>();
            int k = 1;
            double x, y;
            for (int i = 1; i < 7; i++)
            {
                x = Convert.ToDouble(dataGridView1.Rows[0].Cells[i].Value);
                y = Convert.ToDouble(dataGridView1.Rows[1].Cells[i].Value);
                coordinates.Add(x, y);
            }

            foreach (var pair in coordinates)
            {
                dataGridView1.Rows[0].Cells[k].Value = pair.Key;
                dataGridView1.Rows[1].Cells[k].Value = pair.Value;
                k++;
                chart1.Series["Dots"].Points.AddXY(pair.Key, pair.Value);
            }
            chart1.Series["Dots"].Color = Color.Black;
            
            Quad_form(ref chart1, coordinates);
            Hyperbolic_Form(ref chart1, coordinates);
            chart1.Series["Square"].Color = Color.Teal;
            chart1.Series["Hyper"].Color = Color.MediumPurple;
        } 

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
