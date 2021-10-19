using System;
using System.IO.Ports;

namespace LidarDriver
{
    class ArduinoComm
    {
        public static ArduinoComm arduino;

        public EventHandler<StringRecievedEventArgs> StringRecieved;
        SerialPort port;
        public ArduinoComm(string portName, int baudrate)
        {
            arduino = this;

            port = new SerialPort(portName, baudrate, Parity.None, 8, StopBits.One);
            port.DataReceived += ReadString;
            port.Open();
        }

        /// <summary>
        /// This sends a string to the arduino. Arduino will look for a #-symbol followed by the character for dataType
        /// </summary>
        /// <param name="dataType"> Tells the arduino how to interpret the incoming data </param>
        public void SendString(char dataType, string message)
        {
            Console.WriteLine("#" + dataType + message);

            port.Write("#" + dataType);
            port.Write(message);
        }

        public void ReadString(object sender, SerialDataReceivedEventArgs e)
        {
            StringRecievedEventArgs args = new StringRecievedEventArgs(port.ReadLine());
            OnStringRecieved(args);
        }

        protected virtual void OnStringRecieved(StringRecievedEventArgs e)
        {
            StringRecieved?.Invoke(this, e);
        }
    }

    public class StringRecievedEventArgs
    {
        public string line;

        public StringRecievedEventArgs(string message)
        {
            line = message;
        }
    }
}
