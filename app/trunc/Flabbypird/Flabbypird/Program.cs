using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flabbypird
{
    static class Start
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main_()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
        static void Main__()
        {
            Highscore.I.Add("Theodor", 100);
            Highscore.I.Add("Jakob", 200);
        }

        static void Main()
        {
            new _Flabbypird.Game();
        }
    }
}
