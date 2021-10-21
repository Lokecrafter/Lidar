using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace LidarProject
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen p;
        private Pen eraesor;

        const int maxPointsLen = 50;
        LidarData[] points = new LidarData[maxPointsLen];

        public Form1()
        {
            //Defaults the elements of array to have no elements as null
            for (int i = 0; i < maxPointsLen; i++)
            {
                points[i] = new LidarData(0, 0);
            }

            InitializeComponent();

            this.Width = 1500;
            this.Height = 1500;
        }    

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            DrawRobot();

            Loop();
        }

        void Loop() //Loops forever. Didnt figure out how to make use of timer event of forms
        {
            while (true)
            {
                //Program.SimulateJoystick(ArduinoComm.arduino, Input.GetAxis('x'), Input.GetAxis('y'));
                DrawLidarPoint(Program.GetLidarData(ArduinoComm.arduino));
            }
        }

        public void DrawCircle(Point coord, int radius)
        {
            coord.X -= radius;
            coord.Y -= radius;

            g.DrawEllipse(p, new Rectangle(coord, new Size(radius * 2, radius * 2)));
        }
        private void ErasePossible(LidarData lidarData)
        {
            if (lidarData.rendered)
            {
                Point coord = getAngleCoordinates(lidarData.angle, lidarData.distance);

                coord.X -= 1;
                coord.Y -= 1;

                g.DrawEllipse(eraesor, new Rectangle(coord, new Size(2, 2)));
            }
        }
        private void DrawRobot()
        {
            p = new Pen(Color.Red);
            eraesor = new Pen(Color.Black);

            Point midPoint = new Point(this.Width / 2, this.Height / 2);

            DrawCircle(midPoint, 3);
            g.DrawRectangle(p, new Rectangle(midPoint - new Size(7, 4), new Size(14, 18)));

            p = new Pen(Color.White);
        }

        int i = 0; //Iterator to keep track of which point is being drawn or erased.
        public void DrawLidarPoint(LidarData data)
        {
            Point coord = getAngleCoordinates(data.angle, data.distance);

            ErasePossible(points[i]);

            DrawCircle(coord, 1);
            data.rendered = true;
            points[i] = data;

            i++;
            i %= maxPointsLen - 1;

            if (i == 0)
            {
                DrawRobot();
            }
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
            //Program.SimulateJoystick(ArduinoComm.arduino, Input.GetAxis('x'), Input.GetAxis('y'));
        }
    }
}
