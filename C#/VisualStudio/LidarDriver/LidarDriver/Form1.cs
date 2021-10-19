using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LidarDriver
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        Brush b;
        int timerInterval = 5;

        public Form1()
        {
            InitializeComponent();

            this.Width = 1500;
            this.Height = 1500;
            timer1.Interval = timerInterval;
            timer1.Start();

            ArduinoComm.arduino.StringRecieved += DrawLidarPoint;
        }    

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            p = new Pen(Color.Red);
            b = new SolidBrush(Color.Red);

            DrawCircle(new Point(this.Width / 2, this.Height / 2), 5);

            p = new Pen(Color.Black);
        }


        void DrawLidarPoint(object sender, StringRecievedEventArgs e)
        {
            //LidarData data = e.line;
            //Point coord = getAngleCoordinates(data.angle, data.distance);
            //DrawCircle(coord, 1);
        }
        public void DrawCircle(Point coord, int radius)
        {
            coord.X -= radius;
            coord.Y -= radius;

            g.DrawEllipse(p, new Rectangle(coord, new Size(radius * 2, radius * 2)));
        }

        Point getAngleCoordinates(double angle, double distance)
        {
            angle = angle * Math.PI / 180;

            double x = Math.Cos(angle) * distance;
            double y = Math.Sin(angle) * distance;

            Point returnPoint = new Point((int)Math.Round(x) + this.Width / 2, (int)Math.Round(y) + this.Height / 2);
            return (returnPoint);
        }

        //Behaves like an Update or infinite loop function
        private void timer1_Tick(object sender, EventArgs e)
        {
            Program.simulateJoystick(ArduinoComm.arduino, Input.GetAxis('x'), Input.GetAxis('y'));
        }
    }
}
