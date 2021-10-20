using System;
using System.Windows.Forms;
using System.Drawing;

namespace LidarProject
{
    static class Program
    {
        static ArduinoComm arduino;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            arduino = new ArduinoComm("COM8", 115200);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void SimulateJoystick(ArduinoComm ardu, float jx, float jy)
        {
            jx = (float)Math.Round(jx, 2);
            jy = (float)Math.Round(jy, 2);

            string dataLine = jx.ToString() + "," + jy.ToString();
            ardu.SendString('j', dataLine);
            Console.WriteLine("Sent data!");
            if (ardu.ReadString() == "j")
                return;
            else
                return;
        }

        public static LidarData GetLidarData(ArduinoComm ardu)
        {
            ardu.SendString('l');
            string data = ardu.ReadString();
            Console.WriteLine(data);
            return new LidarData(data);
        }
    }
}
