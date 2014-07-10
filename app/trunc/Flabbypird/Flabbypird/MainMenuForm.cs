﻿using System;
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
            //this.Hide();

            //System.Threading.Thread changeLevelThread = new System.Threading.Thread(
            //    new System.Threading.ThreadStart(
            //        () => Application.Run(new /* Form Klasse eintragen */)
            //            ));

            //changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            //changeLevelThread.Start();
            //changeLevelThread.Join();

            //this.Show();
        }

        /// <summary>
        /// Event das ausgeführt wird, wenn der "Hilfe" Button gedrückt wird.
        /// </summary>
        /// <param name="sender">Objekt des Buttons</param>
        /// <param name="e">Eventargumente</param>
        void HelpButton_Click(object sender, EventArgs e)
        {
            //this.Hide();

            //System.Threading.Thread changeLevelThread = new System.Threading.Thread(
            //    new System.Threading.ThreadStart(
            //        () => Application.Run(new /* Form Klasse eintragen */)
            //            ));

            //changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            //changeLevelThread.Start();
            //changeLevelThread.Join();

            //this.Show();
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
            //this.Hide();

            //System.Threading.Thread changeLevelThread = new System.Threading.Thread(
            //    new System.Threading.ThreadStart(
            //        () => Application.Run(new /* Form Klasse eintragen */)
            //            ));

            //changeLevelThread.SetApartmentState(System.Threading.ApartmentState.STA);

            //changeLevelThread.Start();
            //changeLevelThread.Join();

            //this.Show();
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
    }
}