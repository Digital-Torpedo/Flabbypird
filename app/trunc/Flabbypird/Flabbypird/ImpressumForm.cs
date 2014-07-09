using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ImpressumForm : Form
    {
        public ImpressumForm(Point position)
        {
            InitializeComponent();

            this.Text = "Impressum";

            this.StartPosition = FormStartPosition.Manual;

            this.Size = new Size() { Width = 800, Height = 600 };
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            Impressum.Text = "Digital-Torpedo " + Environment.NewLine + 
              "Ekhard Seer e.seer@web.de" + Environment.NewLine + 
"Jakob Warmhold ***keine angaben***" + Environment.NewLine +
"Felix Martin f.martin.1995@web.de" + Environment.NewLine + 
"Theodor Gaede TheodorG@outlook.com";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
