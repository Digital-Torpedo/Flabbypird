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
    public partial class ChangeLevelForm : Form
    {
        public ChangeLevelForm(Point position)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = position;
            
            InitializeComponent();

            this.Size = new Size()
            {
                Width = 800,
                Height = 600
            };

            

            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            /* Level */
            LevelList.SelectedIndexChanged += LevelList_SelectedIndexChanged;

            // Level setzen

            /*  */

        }

        void LevelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // action Levelauswahl
        }
    }
}
