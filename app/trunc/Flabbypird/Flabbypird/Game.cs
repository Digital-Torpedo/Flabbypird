using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Flabbypird
{
    namespace Flabbypird
    {
        internal static class ImageStore
        {
            internal static Image Flabbypird = LoadTexture(@".\images\flabbypird.bmp");
            internal static Image Background = LoadTexture(@".\images\flabbypird.bmp");

            static Image LoadTexture(string path_to_file)
            {
                Bitmap bitmap = new Bitmap(path_to_file);
                bitmap.MakeTransparent(Color.Magenta);
                int id;
                GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

                GL.GenTextures(1, out id);
                GL.BindTexture(TextureTarget.Texture2D, id);

                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);


                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                return new Image(id, bitmap.Width, bitmap.Height);
            }

            internal struct Image
            {
                public int Height;
                public int Width;
                public int ID;

                public Image(int id, int imageWidth, int imageHeight)
                {
                    ID = id;
                    Height = imageHeight;
                    Width = imageWidth;
                }
            }  
        }

        public class Game : GameWindow
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

            void DrawImage(ref int imageID, ref int imageWidth, ref int imageHeight, int positionZ, int positionX, int positionY)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.PushMatrix();
                GL.LoadIdentity();

                GL.Ortho(0, screenWidth, screenHeight, 0, -1, 1);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                GL.LoadIdentity();

                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                GL.Disable(EnableCap.Lighting);

                GL.Enable(EnableCap.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, imageID);

                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 0); // oben links
                GL.Vertex3(0 + positionX, 0 + positionY, 0);

                GL.TexCoord2(1, 0); // oben rechts
                GL.Vertex3(imageWidth + positionX, 0 + positionY, 0);

                GL.TexCoord2(1, 1); // unten rechts
                GL.Vertex3(imageWidth + positionX, imageHeight + positionY, 0);

                GL.TexCoord2(0, 1); // unten links
                GL.Vertex3(0 + positionX, imageHeight + positionY, 0);

                GL.End();

                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Modelview);
            }
        }
    }
}
