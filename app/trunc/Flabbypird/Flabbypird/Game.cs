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
    // Namespace im namespace? nur als Abgetrennter Bereich
    namespace _Flabbypird
    {
        /// <summary>
        /// Store wo sich die Bilder statisch drin befinden und wenn benötigt simpel zugegriffen werden kann.
        /// </summary>
        internal static class ImageStore
        {
            internal static Image Flabbypird = LoadTexture(@".\images\flabbypird1.bmp");
            internal static Image Background = LoadTexture(@".\images\background.bmp");
            internal static Image Barrier = LoadTexture(@".\images\barrier.bmp");

            /// <summary>
            /// Lädt eine Textur und gibt das struct Image zurück
            /// </summary>
            /// <param name="path_to_file"></param>
            /// <returns></returns>
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

            /// <summary>
            /// Struct Image welches Parameter speichert: Höhe, Breite, ID des Images
            /// </summary>
            internal struct Image
            {
                public int Height;
                public int Width;
                public int ID;

                /// <summary>
                /// Initalisierungsmethode
                /// </summary>
                /// <param name="id">ID des Images</param>
                /// <param name="imageWidth">Breite</param>
                /// <param name="imageHeight">Höhe</param>
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
            /// <summary>
            /// Zeichnen einer Textur mit einer Manuellen Skalierung. X und Y Koordinaten im Uhrzeigersinn.
            /// </summary>
            /// <param name="ID"></param>
            /// <param name="positionZ"></param>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            /// <param name="x3"></param>
            /// <param name="y3"></param>
            /// <param name="x4"></param>
            /// <param name="y4"></param>
            public static void Texture(int ID, int positionZ, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
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

                GL.BindTexture(TextureTarget.Texture2D, ID);

                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 0); // oben links
                GL.Vertex3(x1, y1, 0);

                GL.TexCoord2(1, 0); // oben rechts
                GL.Vertex3(x2, y2, 0);

                GL.TexCoord2(1, 1); // unten rechts
                GL.Vertex3(x3, y3, 0);

                GL.TexCoord2(0, 1); // unten links
                GL.Vertex3(x4, y4, 0);

                GL.End();

                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Modelview);
            }

            /// <summary>
            /// Zeichnen der Texture an einem X und Y Punkt ohne Skalierung
            /// </summary>
            /// <param name="Image">Image aus dem ImageStore</param>
            /// <param name="positionZ">Z Position</param>
            /// <param name="positionX">X Position(Links oben)</param>
            /// <param name="positionY">Y Position(Links oben)</param>
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
        }

        /// <summary>
        /// Hauptklasse des Spiels.
        /// </summary>
        public class Game : GameWindow
        {
            #region Barriere
            class Barriers
            {
                class Barrier
                {
                    // X Koordinate der Barriere, A ist die Länge der oberen Barriere, C der Unteren
                    int _X, A, C;

                    /// <summary>
                    /// Public schreibgeschützter zugriff auf _X
                    /// </summary>
                    public int X
                    {
                        get
                        {
                            return _X;
                        }
                    }

                    // Minimale größe einer Barriere vom Außenrand.
                    const int Min = 50;

                    /// <summary>
                    /// Initialisierungsmethode
                    /// </summary>
                    /// <param name="x">Startwert der Koordinate X</param>
                    public Barrier(int x)
                    {
                        _X = x;

                        int MaxSpace = Convert.ToInt16(Settings.I.ScreenHeight * 0.66 - 2 * Min);
                        
                        var rnd = new Random();
                        int tempA = rnd.Next(0, MaxSpace);

                        A = Min + tempA;
                        C = Min + MaxSpace - tempA;
                    }

                    /// <summary>
                    /// Kollissionsüberprüfung
                    /// </summary>
                    /// <param name="a">Eckpunkt der Spielfigur (x, y)</param>
                    /// <param name="b">Eckpunkt der Spielfigur (x, y)</param>
                    /// <param name="c">Eckpunkt der Spielfigur (x, y)</param>
                    /// <param name="d">Eckpunkt der Spielfigur (x, y)</param>
                    /// <returns></returns>
                    internal bool Collission(Point a, Point b, Point c, Point d)
                    {
                        return
                            (a.Y < A && a.X < _X && b.X > _X) ||
                            (d.Y > A + BarrierDistance && d.X < _X && c.X > _X);
                    }

                    /// <summary>
                    /// Bewegung in X Richtung der Barriere durch Update
                    /// </summary>
                    /// <param name="moveX">Wert wie die Barriere in X Richtung verschoben wird</param>
                    /// <returns>Wahrheitswert ob X kleiner als 0 ist.</returns>
                    internal bool Move(int moveX)
                    {
                        _X -= moveX;

                        return _X < 0;
                    }

                    /// <summary>
                    /// Methode die für das Zeichnen der Barriere zuständig ist.
                    /// </summary>
                    internal void Draw()
                    {
                        _Flabbypird.Draw.Texture(ImageStore.Barrier.ID, 2, _X - 12, 0, _X + 12, 0, _X + 12, A, _X - 12, A);

                        int yTop = Convert.ToInt16(Settings.I.ScreenHeight - C);
                        int yBottom = Convert.ToInt16(Settings.I.ScreenHeight);
                        _Flabbypird.Draw.Texture(ImageStore.Barrier.ID, 1,
                            _X - 12, yTop,
                            _X + 12, yTop,
                            _X + 12, yBottom,
                            _X - 12, yBottom);
                    }
                }

                /// <summary>
                /// Initialiserungmethode von allen Barrieren
                /// </summary>
                public Barriers()
                {
                    _Barriers = new List<Barrier>();
                    Add();
                    _Barriers.RemoveRange(0, 3); 
                }

                // Liste der Barrieren
                List<Barrier> _Barriers;

                // Distanz der Barrieren voneinander
                const int BarrierDistance = 275;

                /// <summary>
                /// Überprüft ob die Spielfigur mit einer Barriere Kollidiert
                /// </summary>
                /// <param name="a">Eckpunkt der Spielfigur (x, y)</param>
                /// <param name="b">Eckpunkt der Spielfigur (x, y)</param>
                /// <param name="c">Eckpunkt der Spielfigur (x, y)</param>
                /// <param name="d">Eckpunkt der Spielfigur (x, y)</param>
                /// <returns>Wahrheitswert ob eine Kollission zustande kam</returns>
                internal bool AnyCollision(Point a, Point b, Point c, Point d)
                {
                    return _Barriers.Any(barrier => barrier.Collission(a, b, c, d));
                }

                /// <summary>
                /// Methode die für das Verschieben von allen Barrieren zuständig ist und löscht Barrieren die außerhalb des Bildschirms sind (x < 0)
                /// </summary>
                internal void Update()
                {
                    _Barriers.RemoveAll(x => x.Move(2));
                    Add();
                }

                /// <summary>
                /// Methode die für das Zeichnen aller Barrieren zuständig ist.
                /// </summary>
                internal void Draw()
                {
                    foreach (var barrier in _Barriers)
                        barrier.Draw();
                }

                /// <summary>
                /// Füllt Automatisch so viele Barrieren auf wie der Bildschirm platz bietet.
                /// </summary>
                void Add()
                {
                    do
                        _Barriers.Add(new Barrier(_Barriers.Count > 0 ? _Barriers.Last().X + BarrierDistance : BarrierDistance));
                    while (_Barriers.Last().X < Settings.I.ScreenWidth); 
                }
            }
            #endregion

            #region Player

            /// <summary>
            /// Klasse welche für die Spielbare Figur zuständig ist.
            /// </summary>
            public class Player
            {
                // X Achse und die Vier Koordinaten im Uhrzeigersinn der Spielfigur (x & y).
                static int X = Convert.ToInt16(Settings.I.ScreenWidth / 3);
                public Point A = new Point(X - ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) - ImageStore.Flabbypird.Height / 2);
                public Point B = new Point(X + ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) - ImageStore.Flabbypird.Height / 2);
                public Point C = new Point(X + ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) + ImageStore.Flabbypird.Height / 2);
                public Point D = new Point(X - ImageStore.Flabbypird.Width / 2, Convert.ToInt16(Settings.I.ScreenHeight / 2) + ImageStore.Flabbypird.Height / 2);

                /// <summary>
                /// Methode welche die vier Y Koordinaten der SPielfigur bewegt.
                /// </summary>
                /// <param name="moveY"></param>
                internal void Move(int moveY)
                {
                    A.Y += moveY;
                    B.Y += moveY;
                    C.Y += moveY;
                    D.Y += moveY;
                }

                // Gravitation Hoch wenn man die Leertaste drückt, und automatische Fallgravitation.
                const double GravitationUp = 120.0 / 60;
                const double GravitationDown = 4.0 / 60;
                double FallSpeed = 0.0;

                /// <summary>
                /// Update Methode die vom Hauptfenster in der Update Methode aufgerufen wird.
                /// </summary>
                internal void Update()
                {
                    Move(Convert.ToInt16(FallSpeed));
                    FallSpeed += GravitationDown;
                }

                /// <summary>
                /// Mit dieser Methode ändert man die Gravitation auf die GravitationUp Geschwindigkeit.
                /// </summary>
                internal void Jump()
                {
                    FallSpeed = -GravitationUp;
                }

                /// <summary>
                /// Methode die für das Zeichnen der Spielfigur zuständig ist.
                /// </summary>
                internal void Draw()
                {
                    _Flabbypird.Draw.Image(ImageStore.Flabbypird, 0, A.X, A.Y);  
                }
            }
            #endregion

            #region Background

            /// <summary>
            /// Klasse welche den  Hintergrund verantwortlich ist.
            /// </summary>
            class Background
            {

                internal void Draw()
                {
                    int xMax = Convert.ToInt16(Settings.I.ScreenWidth);
                    int yMax = Convert.ToInt16(Settings.I.ScreenHeight);
                    _Flabbypird.Draw.Texture(
                        ImageStore.Background.ID, 3,
                        0, 0,
                        xMax, 0,
                        xMax, yMax,
                        0, yMax
                        );
                }
            }
            #endregion

            Player _Player;
            Barriers _Barriers;
            Background _Background;
            bool GameStarted = false;

            /// <summary>
            /// Initialisierungsmethode.
            /// </summary>
            public Game() : base(Convert.ToInt16(Settings.I.ScreenWidth), Convert.ToInt16(Settings.I.ScreenHeight))
            {
                Load += game_Load;
                Resize += game_Resize;
                UpdateFrame += game_UpdateFrame;
                RenderFrame += game_RenderFrame;

                Keyboard.KeyDown += Keyboard_KeyDown;
                Keyboard.KeyDown += Keyboard_KeyDown_StartGame;
                Keyboard.KeyUp += Keyboard_KeyUp;

                Run(60.0, 60.0);
            }

            // Sperrt die Leertaste gedrückt Funktion.
            bool SpaceLock = false;

            /// <summary>
            /// Dieses Event wird ausgelöst wenn man eine Taste drückt.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e">Eventargumente</param>
            void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
            {
                if (!SpaceLock && e.Key == Key.Space)
                    _Player.Jump();

                SpaceLock = true;
            }

            /// <summary>
            /// Dieses Event wird ausgelöst wenn man eine Taste drückt. Die FUnktion wird nur ein mal ausgeführt.
            /// Sie startet außerdem das Spiel.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e">Eventargumente</param>
            void Keyboard_KeyDown_StartGame(object sender, KeyboardKeyEventArgs e)
            {
                GameStarted = true;
                Keyboard.KeyDown -= Keyboard_KeyDown_StartGame;
            }

            /// <summary>
            /// Event welches eintritt, wenn eine Taste losgelassen wurde.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
            {
                if (e.Key == Key.Space)
                    SpaceLock = false;
            }

            /// <summary>
            /// Methode die für die Darstellung zuständig ist.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void game_RenderFrame(object sender, FrameEventArgs e)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                 _Background.Draw();
                _Barriers.Draw();
                _Player.Draw();
             

                SwapBuffers();
            }

            /// <summary>
            /// Methode die für das aktualisieren der Spiellogic zuständig ist.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void game_UpdateFrame(object sender, FrameEventArgs e)
            {
                if (GameStarted)
                    if (_Barriers.AnyCollision(_Player.A, _Player.B, _Player.C, _Player.D)) 
                        this.Close();
                    else
                    {
                        _Player.Update();
                        _Barriers.Update();
                    }
                        
                else
                    GameStarted = Keyboard[Key.Space];
            }
            
            /// <summary>
            /// Methode welche ausgeführt wird wenn vom Fenster die Größe verändert wird.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void game_Resize(object sender, EventArgs e)
            {
                GL.Viewport(0, 0, Width, Height);
            }

            // Methode die aufgerufen wird, wenn das Fenster geladen wird.
            void game_Load(object sender, EventArgs e)
            {
                GL.LineWidth(4f);
                VSync = VSyncMode.On;
                _Player = new Player();
                _Barriers = new Barriers();
                _Background = new Background();
            }
        }
    }
}
