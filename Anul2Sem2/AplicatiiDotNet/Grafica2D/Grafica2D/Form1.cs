using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica2D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics graphics;
        Bitmap bitmap;
        Random random;
        // ...
        Point center = new Point();

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            random = new Random();
            // ...
            center = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            DrawXoYAxes();

            graphics.DrawLine(new Pen(Color.Black, 2), GetMappedPoint(), GetMappedPoint(100, 100));
            graphics.DrawLine(new Pen(Color.Blue, 3), GetMappedPoint(50, 60), GetMappedPoint(150, 80));
            graphics.DrawLine(
                new Pen(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)), random.Next(10)),
                GetRandomMappedPoint(),
                GetRandomMappedPoint());
            // ...
            graphics.FillRectangle(new SolidBrush(Color.ForestGreen), 0, 0, center.X, center.Y);
            Point[] points = new Point[] { GetRandomMappedPoint(), GetRandomMappedPoint(), GetRandomMappedPoint(), GetRandomMappedPoint(), GetRandomMappedPoint() };
            graphics.FillPolygon(
                new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255))),
                points);
            graphics.DrawEllipse(Pens.Red, center.X - (center.Y / 2), center.Y / 2, center.Y, center.Y);

            pictureBox1.Image = bitmap;
        }

        private void DrawXoYAxes()
        {
            Pen pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Black);
            Font font = new Font("Arial", 12, FontStyle.Bold);

            graphics.DrawLine(pen, GetMappedPoint(-center.X, 0), GetMappedPoint(center.X, 0));
            graphics.DrawLine(pen, GetMappedPoint(0, -center.Y), GetMappedPoint(0, center.Y));
            graphics.DrawLine(pen, GetMappedPoint(center.X - 20, 10), GetMappedPoint(center.X, 0));
            graphics.DrawLine(pen, GetMappedPoint(center.X - 20, -10), GetMappedPoint(center.X, 0));
            graphics.DrawLine(pen, GetMappedPoint(10, center.Y - 20), GetMappedPoint(0, center.Y));
            graphics.DrawLine(pen, GetMappedPoint(-10, center.Y - 20), GetMappedPoint(0, center.Y));

            graphics.DrawString("X", font, brush, GetMappedPoint(center.X - 20, -10));
            graphics.DrawString("O", font, brush, center);
            graphics.DrawString("Y", font, brush, GetMappedPoint(10, center.Y));
        }

        private Point GetMappedPoint(int x, int y) => new Point(center.X + x, center.Y - y);
        private Point GetMappedPoint() => GetMappedPoint(0, 0);
        private Point GetRandomMappedPoint() =>
            GetMappedPoint(random.Next(-center.X, center.X), random.Next(-center.Y, center.Y));
    }
}
