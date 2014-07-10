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
    namespace _Flabbypird
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

        static class Draw
        {
            public static void Image(ImageStore.Image Image, int positionZ, int positionX, int positionY)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.PushMatrix();
                GL.LoadIdentity();

                GL.Ortho(0, Settings.I.ScreenWidth, Settings.I.ScreenHeight, 0, -1, 1);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                GL.LoadIdentity();

                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                GL.Disable(EnableCap.Lighting);

                GL.Enable(EnableCap.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, Image.ID);

                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 0); // oben links
                GL.Vertex3(0 + positionX, 0 + positionY, 0);

                GL.TexCoord2(1, 0); // oben rechts
                GL.Vertex3(Image.Width + positionX, 0 + positionY, 0);

                GL.TexCoord2(1, 1); // unten rechts
                GL.Vertex3(Image.Width + positionX, Image.Height + positionY, 0);

                GL.TexCoord2(0, 1); // unten links
                GL.Vertex3(0 + positionX, Image.Height + positionY, 0);

                GL.End();

                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Modelview);
            }

            public static void Line(float x1, float y1, float x2, float y2)
            {
                GL.Begin(BeginMode.Lines);
                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);
                GL.End();
            }
        }

        public class Game : GameWindow
        {
            class Barriers
            {
                class Barrier
                {
                    int X, A, C;

                    const int Min = 50;

                    public Barrier(int x)
                    {
                        X = x;

                        int MaxSpace = Convert.ToInt16(Settings.I.ScreenHeight * (2.0 / 3.0) - 2 * Min);
                        
                        var rnd = new Random();
                        int tempA = rnd.Next(0, MaxSpace);

                        A = Min + tempA;
                        C = Min + MaxSpace - tempA;
                    }

                    internal bool Collission(Point a, Point b, Point c, Point d)
                    {
                        return
                            (a.Y < A && a.X < X && b.X > X) ||
                            (d.Y < C && d.X < X && c.X > X);
                    }

                    internal bool Move(int moveX)
                    {
                        X -= moveX;

                        return X < 0;
                    }

                    internal void Draw()
                    {
                        GL.Color3(Color.Red);
                        _Flabbypird.Draw.Line((float)X, (float)A, (float)X, (float)Settings.I.ScreenHeight);
                    }
                }

                public Barriers()
                {
                    Add(Convert.ToInt16(Settings.I.ScreenWidth) / BarrierDistance);
                }

                List<Barrier> _Barriers;

                const int BarrierDistance = 150;

                internal bool AnyCollision(Point a, Point b, Point c, Point d)
                {
                    return _Barriers.Any(barrier => barrier.Collission(a, b, c, d));
                }

                internal void Update()
                {
                    _Barriers.RemoveAll(x => x.Move(20));
                    Add(Convert.ToInt16(Settings.I.ScreenWidth) / BarrierDistance - _Barriers.Count);
                }

                internal void Draw()
                {
                    foreach (var barrier in _Barriers)
                        barrier.Draw();
                }

                void Add(int count)
                {
                    for (int b = 0; b < count; b++)
                        _Barriers.Add(new Barrier(BarrierDistance * b));
                }
            }

            public class Player
            {
                static int X = Convert.ToInt16(Settings.I.ScreenWidth / 3);
                public Point A = new Point(X - ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) - ImageStore.Flabbypird.Height / 2);
                public Point B = new Point(X + ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) - ImageStore.Flabbypird.Height / 2);
                public Point C = new Point(X + ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) + ImageStore.Flabbypird.Height / 2);
                public Point D = new Point(X - ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) + ImageStore.Flabbypird.Height / 2);

                internal void Move(int moveY)
                {
                    A.Y += moveY;
                    B.Y += moveY;
                    C.Y += moveY;
                    D.Y += moveY;
                }

                const double GravitationUp = 25;
                const double GravitationDown = 10;
                double FallSpeed = 0.0;

                internal void Update(bool Pressed)
                {
                    FallSpeed += GravitationDown;

                    if (Pressed)
                        FallSpeed -= GravitationDown;

                    Move(Convert.ToInt16(FallSpeed));
                } 

                internal void Draw()
                {
                    _Flabbypird.Draw.Image(ImageStore.Flabbypird, 3, X, A.Y);
                }
            }

            Player _Player;
            Barriers _Barriers;
            bool GameStarted = false;

            public Game() : base(Convert.ToInt16(Settings.I.ScreenWidth), Convert.ToInt16(Settings.I.ScreenHeight))
            {
                Load += game_Load;
                Resize += game_Resize;
                UpdateFrame += game_UpdateFrame;
                RenderFrame += game_RenderFrame;

                Run(60.0, 60.0);
            }

            void game_RenderFrame(object sender, FrameEventArgs e)
            {
                _Barriers.Draw();
                _Player.Draw();
            }

            void game_UpdateFrame(object sender, FrameEventArgs e)
            {
                if (GameStarted)
                    if (_Barriers.AnyCollision(_Player.A, _Player.B, _Player.C, _Player.D))
                        this.Close();
                    else
                    {
                        _Player.Update(Keyboard[Key.Space]);
                    }
            }
            
            void game_Resize(object sender, EventArgs e)
            {

            }

            void game_Load(object sender, EventArgs e)
            {
                _Player = new Player();
                _Barriers = new Barriers();
            }
        }
    }
}
