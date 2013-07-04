using System;
using System.Threading;
using System.Windows.Forms;

namespace Sc2Hack.Classes.BackEnds
{
    class Simulation
    {
        public static void Keyboard_SimulateKey(IntPtr handle, Keys key, Int32 times)
        {
            for (var i = 0; i < times; i++)
            {
                /* Key Down */
                InteropCalls.SendMessage(handle, (uint)InteropCalls.WMessages.Keydown, (IntPtr)key, IntPtr.Zero);

                /* Key Up */
                InteropCalls.SendMessage(handle, (uint)InteropCalls.WMessages.Keyup, (IntPtr)key, IntPtr.Zero);

                Thread.Sleep(1);
            }
        }
    }
}
