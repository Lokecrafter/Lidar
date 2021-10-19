using System;
using System.Windows.Input;

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stirting!");

            ArduinoCom arduino = new ArduinoCom("COM5", 115200);

            arduino.StringRecieved += PrintSerialData;

            while(true){
                
                ConsoleKeyInfo key = Console.ReadKey();

                if(ConsoleKey.W == key.Key){
                    inputJoystick(arduino, 0f, 1f);
                }
            }
        }
        static void PrintSerialData(object source, StringRecievedEventArgs e){
            //Console.WriteLine(e.line);
        }


        static void inputJoystick(ArduinoCom ardu, float jx, float jy) {
            jx = (float)Math.Round(jx, 2);
            jy = (float)Math.Round(jy, 2);

            string dataLine = jx.ToString() + "," + jy.ToString();
            Console.WriteLine(dataLine);
            ardu.SendString('j', dataLine);
        }
    }
}