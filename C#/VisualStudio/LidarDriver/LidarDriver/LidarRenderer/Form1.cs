using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LidarRenderer
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        Brush b;

        public Form1()
        {
            InitializeComponent();

            this.Width = 1500;
            this.Height = 1500;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            p = new Pen(Color.Red);
            b = new SolidBrush(Color.Red);

            DrawCircle(new Point(this.Width / 2, this.Height / 2), 5);

            p = new Pen(Color.Black);

            Loop();
        }

        public void DrawCircle(Point coord, int radius)
        {
            coord.X -= radius;
            coord.Y -= radius;

            g.DrawEllipse(p, new Rectangle(coord, new Size(radius*2, radius * 2)));
        }

        void Loop()
        {
            while (true)
            {
                LidarData data = Serial.singleton.GetData();

                Point coord = getAngleCoordinates(data.angle, data.distance);

                DrawCircle(coord, 1);
            }
        }

        Point getAngleCoordinates(double angle, double distance)
        {
            angle = angle * Math.PI / 180;

            double x = Math.Cos(angle) * distance;
            double y = Math.Sin(angle) * distance;

            Point returnPoint = new Point((int)Math.Round(x) + this.Width / 2, (int)Math.Round(y) + this.Height/2);
            return (returnPoint);
        }
    }
}