using System;
using System.Windows.Forms;

namespace LidarRenderer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();
            Serial serial = new Serial("COM8", 115200, 2);

            Application.Run(form);

            
        }
    }
}






