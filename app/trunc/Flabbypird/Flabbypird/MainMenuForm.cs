using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flabbypird
{
    public partial class MainMenuForm : Form
    {
        /// <summary>
        /// Initialisierungsmethode
        /// </summary>
        public MainMenuForm()
        {
            InitializeComponent();

            trackBar1.Value = Convert.ToInt32(Settings.I.Volume * 100);

            volumeLabel.Text = Convert.ToString(trackBar1.Value);

            this.Size = new Size()
            {
                Width = 800,
                Height = 600
            };

            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.Text = "Flabbypird : Hauptmenü";

            /* HighScore */
            HighScoreButton.Text = "Bestenliste";
            HighScoreButton.Click += HighScoreButton_Click;

            /* Impressum */
            ImpressumButtom.Text = "Impressum";
            ImpressumButtom.Click += ImpressumButtom_Click;

            /* Close */
            CloseButton.Text = "Schließen";
            CloseButton.Click += CloseButton_Click;

            /* Help */
            HelpButton.Text = "Hilfe";
            HelpButton.Click += HelpButton_Click;

            /* Continue Game Button */
            GameButton.Text = "Spielen";
            GameButton.Click += GameButton_Click;

        }
            
        /// <summary>
        /// Event das ausgeführt wird, wenn der "Spiel Starten" Button gedrückt wird.
        /// </summary>
        /// <param name="sender">Objekt des Buttons</param>
        /// <param name="e">Eventargumente</param>
        void GameButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            System.Threading.Thread changeLevelThread = new System.Threading.Thread(
                new System.Threading.ThreadStart(
                    () => new Game.Window()
                        ));

            changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            changeLevelThread.Start();
            changeLevelThread.Join();

            this.Show();
        }

        /// <summary>
        /// Event das ausgeführt wird, wenn der "Hilfe" Button gedrückt wird.
        /// </summary>
        /// <param name="sender">Objekt des Buttons</param>
        /// <param name="e">Eventargumente</param>
        void HelpButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            System.Threading.Thread changeLevelThread = new System.Threading.Thread(
                new System.Threading.ThreadStart(
                    () => Application.Run(new HelpForm(this.Location))
                        ));

            changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            changeLevelThread.Start();
            changeLevelThread.Join();

            this.Show();
        }

        /// <summary>
        /// Event das ausgeführt wird, wenn der "Impressum" Button gedrückt wird.
        /// </summary>
        /// <param name="sender">Objekt des Buttons</param>
        /// <param name="e">Eventargumente</param>
        void ImpressumButtom_Click(object sender, EventArgs e)
        {
            this.Hide();

            System.Threading.Thread changeLevelThread = new System.Threading.Thread(
                new System.Threading.ThreadStart(
                    () => Application.Run(new ImpressumForm(this.Location))
                        ));

            changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            changeLevelThread.Start();
            changeLevelThread.Join();

            this.Show();
        }

        /// <summary>
        /// Event das ausgeführt wird, wenn der "Bestenliste" Button gedrückt wird.
        /// </summary>
        /// <param name="sender">Objekt des Buttons</param>
        /// <param name="e">Eventargumente</param>
        void HighScoreButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            System.Threading.Thread changeLevelThread = new System.Threading.Thread(
                new System.Threading.ThreadStart(
                    () => Application.Run(new HighscoreForm(this.Location))
                        ));

            changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            changeLevelThread.Start();
            changeLevelThread.Join();

            this.Show();
        }

        /// <summary>
        /// Event für den Schließen Button beim klicken.
        /// </summary>
        /// <param name="sender">Objekt des Buttons</param>
        /// <param name="e">Eventargumente</param>
        void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Event, das ausgeführt wird, wenn sich der Wert des Lautstärkereglers änder. Dieser wird direkt in einem Label angezeigt
        /// </summary>
        /// <param name="sender">Objekt der TrackBar</param>
        /// <param name="e">Eventargumente</param>
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            volumeLabel.Text = Convert.ToString(trackBar1.Value);
        }

        /// <summary>
        /// Event, das ausgeführt wird, wenn sich der Zustand der Checkbox ändert. Wird diese aktiviert, wird der Lautstärkeregler deaktiviert. Und Umgekehrt.
        /// </summary>
        /// <param name="sender">Objekt der Checkbox</param>
        /// <param name="e">Eventargumente</param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox1.Checked)
            {
                case true:
                    trackBar1.Enabled = false;
                    Settings.I.VolumeEnabled = false;
                    break;
                case false:
                    trackBar1.Enabled = true;
                    Settings.I.VolumeEnabled = true;
                    break;
            }
        }

        /// <summary>
        /// Event, das ausgeführt wird, wenn der Lautstärkeregler losgelassen wird.
        /// </summary>
        /// <param name="sender">Objekt der TrackBar</param>
        /// <param name="e">Eventargumente</param>
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            Settings.I.Volume = Convert.ToDouble(trackBar1.Value) / 100;
        }
    }
}
