using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sc2Hack_UpdateManager.Classes.Fontend;

namespace Sc2Hack_UpdateManager
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
            Application.Run(new MainWindow());
        }
    }
}
