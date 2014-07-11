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
    public partial class AddHighScoreFailedForm : Form
    {
        public AddHighScoreFailedForm()
        {
            InitializeComponent();
            backButton.Click += backButton_Click;
            retryButton.Click += retryButton_Click;
        }

        void retryButton_Click(object sender, EventArgs e)
        {
            new _Flabbypird.Game();
        }

        void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
