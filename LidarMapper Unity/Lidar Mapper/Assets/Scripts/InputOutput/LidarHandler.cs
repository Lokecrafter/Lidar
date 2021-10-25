using System.IO.Ports;
using UnityEngine;

namespace LidarProject
{
    public class LidarHandler : MonoBehaviour
    {
        public static LidarHandler singleton;

        private ArduinoComm arduino;
        [SerializeField] private LidarRenderer pointRenderer;

        public void OpenNewSerialPort(string portName, int baudRate)
        {
            if (arduino.Port.IsOpen)
            {
                arduino.Port.Close();
            }
            SerialPort newPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            newPort.Open();

            arduino.Port = newPort;
        }

        void Awake()
        {
            singleton = this;
            arduino = new ArduinoComm();

            arduino.Port = new SerialPort("COM9", 2000000);

            OpenNewSerialPort("COM9", 2000000);
        }

        void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                SimulateJoystick(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                LidarData data = GetLidarData();
                pointRenderer.RenderLidarPoint(data);
            }
        }

        public void SimulateJoystick(float jx, float jy)
        {
            int ijx = Mathf.RoundToInt(jx);
            int ijy = Mathf.RoundToInt(jy);

            string dataLine = ijx + "," + ijy;
            arduino.SendString(dataLine);
        }

        public LidarData GetLidarData()
        {
            string data = arduino.ReadString();
            return new LidarData(data);
        }
    }
}