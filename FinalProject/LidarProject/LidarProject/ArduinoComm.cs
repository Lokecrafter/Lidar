using System;
using System.IO.Ports;

namespace LidarProject
{
    class ArduinoComm
    {
        public static ArduinoComm arduino;

        SerialPort port;
        public ArduinoComm(string portName, int baudrate)
        {
            arduino = this;

            port = new SerialPort(portName, baudrate, Parity.None, 8, StopBits.One);
            port.Open();
        }

        /// <summary>
        /// This sends a string to the arduino. Arduino will look for a #-symbol followed by the character for dataType
        /// </summary>
        /// <param name="dataType"> Tells the arduino how to interpret the incoming data </param>
        public void SendString(char dataType, string message = null)
        {
            Console.WriteLine("#" + dataType + message);

            port.Write("#" + dataType);
            if(message != null)
                port.Write(message);
        }

        public string ReadString()
        {
            return port.ReadLine();
        }

        public void ClearBuffer()
        {
            port.DiscardInBuffer();
        }
    }
}
