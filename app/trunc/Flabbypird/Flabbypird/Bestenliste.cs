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
    public partial class BestenlisteForm : Form
    {
        /// <summary>
        /// Stellt die Bestenliste als Form da
        /// </summary>
        public BestenlisteForm()
        {
            InitializeComponent();
            this.Text = "Bestenliste";
            this.Size = new Size()
            {
                Width = 800,
                Height = 600
            };
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Location = new Point();
            //label1.Text = Highscore.All();
            label1.Text = "Hans Wurst";
        }

        /// <summary>
        /// Event das Ausgeführt wird wenn der Button "Zurück" geclickt wird
        /// </summary>
        /// <param name="sender">sender objekt</param>
        /// <param name="e">event argument</param>
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
