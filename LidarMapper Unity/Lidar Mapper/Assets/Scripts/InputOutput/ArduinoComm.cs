using System;
using System.IO.Ports;
using UnityEngine;

namespace LidarProject
{
    public class ArduinoComm
    {
        public static ArduinoComm arduino;
        public SerialPort Port { get; set; }

        public ArduinoComm()
        {
            arduino = this;
        }

        public void SendString(string message)
        {
            Port.Write(message);
        }

        public string ReadString()
        {
            return Port.ReadLine();
        }
    }
}
