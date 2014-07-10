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
    public partial class Hilfe : Form
    {
        /// <summary>
        /// Stellt die Form da und gibt den "Ziel des Spiels" Text aus "
        /// </summary>
        public Hilfe(Point location)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Text = "Hilfe";
            this.Size = new Size()
            {
                Width = 800,
                Height = 600
            };
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Location = location;
            pictureBox1.Hide();
            label1.Text = "Das Ziel des Spiels ist es soweit wie möglich" + Environment.NewLine +
                          "im Level voranzukommen ohne mit der" + Environment.NewLine +
                          "Spielumgebung in Kontakt zu kommen";
        }


        /// <summary>
        ///  Event das ausgeführt wird wenn der "Zurück" Button gedrückt wird
        /// </summary>
        /// <param name="sender">sender objekt</param>
        /// <param name="e">event argument</param>
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Event das ausgeführt wird wenn der "Ziel des Spiels" Button gedrückt wird
        /// </summary>
        /// <param name="sender">sender objekt</param>
        /// <param name="e">event argument</param>
        private void goal_Click(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            label1.Text = "Das Ziel des Spiels ist es soweit wie möglich" + Environment.NewLine +
                          "im Level voranzukommen ohne mit der" + Environment.NewLine +
                          "Spielumgebung in Kontakt zu kommen";
        }

        /// <summary>
        /// Event das ausgeführt wird wenn der "Steuerung" Button gedrückt wird
        /// </summary>
        /// <param name="sender">sender objekt</param>
        /// <param name="e">event argument</param>
        private void control_Click(object sender, EventArgs e)
        {
            pictureBox1.Show();
            label1.Text = "Drücken Sie wiederholt die Leertaste um zu flattern.";
        }
    }
}
