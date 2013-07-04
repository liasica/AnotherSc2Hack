using System;
using System.Windows.Forms;
using Sc2Hack.Classes.FontEnds;

namespace Sc2Hack
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainHandler());
        }
    }
}
