using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Flabbypird
{
    public partial class ImpressumForm : Form
    {
        /// <summary>
        /// Initialisierungsmethode.
        /// </summary>
        /// <param name="location">Argument welche Position das Vaterfenster hat.</param>
        public ImpressumForm(Point location)
        {
            InitializeComponent();

            this.Text = "Impressum";

            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;

            this.Size = new Size() { Width = 800, Height = 600 };
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            Impressum.Text =
                "Digital-Torpedo" + Environment.NewLine + 
                "Ekhard Seer e.seer@web.de" + Environment.NewLine + 
                "Jakob Warmhold jakob@warmhold.de" + Environment.NewLine +
                "Felix Martin f.martin.1995@web.de" + Environment.NewLine + 
                "Theodor Gaede TheodorG@outlook.com";
        }

        /// <summary>
        /// Event welches das Fenster schließt.
        /// </summary>
        /// <param name="sender">sender objekt</param>
        /// <param name="e">event argument</param>
        void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
