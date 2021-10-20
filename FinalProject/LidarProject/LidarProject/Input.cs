using WindowsInput.Native;
using WindowsInput;

namespace LidarProject
{
    static class Input
    {
        private static InputSimulator sim = new InputSimulator();

        public static float GetAxis(char axisName)
        {
            if (axisName == 'y')
            {
                float retVal = 0f;
                if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_W)) retVal++;
                if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_S)) retVal--;

                return retVal;
            }
            else if (axisName == 'x')
            {
                float retVal = 0f;
                if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_D)) retVal++;
                if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_A)) retVal--;

                return retVal;
            }
            else return 0;
        }
    }
}
