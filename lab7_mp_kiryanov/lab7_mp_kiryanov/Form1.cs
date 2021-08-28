using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_mp_kiryanov
{
    public partial class Form1 : Form
    {
        // константы
        const double a = 0;
        const double b = 1;
        const double x0 = 0;
        const double y0 = 2;
        const double h = 0.1;

        int step;

        double[] x_coords;
        double[] y_coords;
        public Form1()
        {
            InitializeComponent();
        }

        public double func(double x, double y)
        {
            return x * x - y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            step = (int)(Math.Abs(a - b) / h);

            this.chart1.Series[0].Points.Clear();
            // table_2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            x_coords = new double[step];
            y_coords = new double[step];

            x_coords[0] = x0;
            y_coords[0] = y0;

            for (int k = 1; k < step; k++)
            {
                x_coords[k] = x0 + k * h;
                y_coords[k] = y_coords[k - 1] + h * func(x_coords[k - 1], y_coords[k - 1]);
            }

            for (int i = 0; i < step; i++)
                this.chart1.Series[0].Points.AddXY(x_coords[i], y_coords[i]);

            dataGridView1.RowCount = step;
            dataGridView1.ColumnCount = 3;

            for (int i = 0; i < step; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i;
                dataGridView1.Rows[i].Cells[1].Value = x_coords[i];
                dataGridView1.Rows[i].Cells[2].Value = y_coords[i];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Visible = true;
            this.chart2.Series[0].Points.Clear();

            //table_2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            // table_2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            step = (int)(Math.Abs(a - b) / h);

            x_coords = new double[step];
            y_coords = new double[step];

            double[] y_tmp1 = new double[step];
            double[] y_tmp2 = new double[step];
            double[] y_tmp3 = new double[step];
            double[] y_tmp4 = new double[step];

            x_coords[0] = x0;
            y_coords[0] = y0;

            for (int k = 1; k < step; k++)
            {
                x_coords[k] = a + k * h;
                y_tmp1[k] = h * func(x_coords[k - 1], y_coords[k - 1]);
                y_tmp2[k] = h * func(x_coords[k - 1] + h / 2.0, y_coords[k - 1] + y_tmp1[k] / 2.0);
                y_tmp3[k] = h * func(x_coords[k - 1] + h / 2, y_coords[k - 1] + y_tmp2[k] / 2);
                y_tmp4[k] = h * func(x_coords[k - 1] + h, y_coords[k - 1] + y_tmp3[k]);
                y_coords[k] = y_coords[k - 1] + (y_tmp1[k] + 2 * y_tmp2[k] + 2 * y_tmp3[k] + y_tmp4[k]) / 6;
            }

            for (int i = 0; i < step; i++)
                this.chart2.Series[0].Points.AddXY(x_coords[i], y_coords[i]);

            dataGridView2.RowCount = step;
            dataGridView2.ColumnCount = 3;

            for (int i = 0; i < step; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = i;
                dataGridView2.Rows[i].Cells[1].Value = x_coords[i];
                dataGridView2.Rows[i].Cells[2].Value = y_coords[i];
            }
        }
    }
}
