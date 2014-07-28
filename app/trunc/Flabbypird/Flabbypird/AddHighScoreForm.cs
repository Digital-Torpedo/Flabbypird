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
    public partial class AddHighScoreForm : Form
    {
        int Score;
        public AddHighScoreForm(int score)
        {
            this.Score = score;
            InitializeComponent();

            this.Size = new Size()
            {
                Width = 800,
                Height = 600
            };

            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            nameBox.Click += nameBox_Enter;
            applyButton.Click += applyButton_Click;
        }

        void applyButton_Click(object sender, EventArgs e)
        {
            this.Close();

            if (nameBox.Text != "")
                Highscore.I.Add(nameBox.Text, Score);

            Application.Run(
                new HighscoreForm(this.Location)
                );
        }

        void nameBox_Enter(object sender, EventArgs e)
        {
            this.Close();

            if (nameBox.Text != "")
                Highscore.I.Add(nameBox.Text, Score);

            new Game.Window();
        }
    }
}
