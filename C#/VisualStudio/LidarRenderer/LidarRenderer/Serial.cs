using System;
using System.IO.Ports;

namespace LidarRenderer
{
    public class Serial
    {
        public static Serial singleton;

        private SerialPort port;
        private int byteSize;

        public Serial(string portName, int baudRate, int byteSize)
        {
            singleton = this;

            this.port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            this.byteSize = byteSize;

            SerialInitzialise();
        }

        public void SerialInitzialise()
        {
            Console.WriteLine("Incoming Data:");


            // Begin communications
            port.Open();

            port.DiscardInBuffer();
        }


        public LidarData GetData()
        {;
            port.Write(" ");

            string recievedString = port.ReadLine();

            string[] datas = recievedString.Split(',');


            //Console.WriteLine(int.Parse(datas[0]) + ", " + int.Parse(datas[1]));
            LidarData data = new LidarData(int.Parse(datas[0]), int.Parse(datas[1]));

            return data;
        }

        public void inputJoystick(float jx, float jy)
        {
            string dataLine = jx + "," + jy;


            port.WriteLine(dataLine);
        }
    }
}