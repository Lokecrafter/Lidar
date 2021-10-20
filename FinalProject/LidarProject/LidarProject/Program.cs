using System;
using System.Windows.Forms;

namespace LidarProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ArduinoComm arduino = new ArduinoComm("COM8", 115200);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void simulateJoystick(ArduinoComm ardu, float jx, float jy)
        {
            jx = (float)Math.Round(jx, 2);
            jy = (float)Math.Round(jy, 2);

            string dataLine = jx.ToString() + "," + jy.ToString();
            Console.WriteLine(dataLine);
            ardu.SendString('j', dataLine);
        }
    }
}
