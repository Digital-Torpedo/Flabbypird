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
    public partial class HighscoreForm : Form
    {
        /// <summary>
        /// Stellt die Bestenliste als Form da
        /// </summary>
        public HighscoreForm(Point location)
        {
            InitializeComponent();
            this.Text = "Bestenliste";

            this.Size = new Size()
            {
                Width = 800,
                Height = 600
            };

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Location = location;

            label1.Text = Highscore.I.All();
        }

        /// <summary>
        /// Event das Ausgeführt wird wenn der Button "Zurück" geklickt wurde
        /// </summary>
        /// <param name="sender">sender objekt</param>
        /// <param name="e">event argument</param>
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
