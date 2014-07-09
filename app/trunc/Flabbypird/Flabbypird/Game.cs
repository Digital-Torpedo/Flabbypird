using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using OpenTK;

namespace Flabbypird
{
    class Game : GameWindow
    {
        public Game() //: base()
        {
            Load += game_Load;
            Resize += game_Resize;
            UpdateFrame += game_UpdateFrame;
            RenderFrame += game_RenderFrame;

            Run(60.0, 60.0);
        }

        void game_RenderFrame(object sender, FrameEventArgs e)
        {
            
        }

        void game_UpdateFrame(object sender, FrameEventArgs e)
        {
            
        }

        void game_Resize(object sender, EventArgs e)
        {
            
        }

        void game_Load(object sender, EventArgs e)
        {
            
        }
    }
}
