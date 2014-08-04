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

using Settings;

namespace Game
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
        /// Initialisiert die Images neu.
        /// </summary>
        internal static void Initialize()
        {
            Flabbypird = LoadTexture(@".\images\flabbypird1.bmp");
            Background = LoadTexture(@".\images\background.bmp");
            Barrier = LoadTexture(@".\images\barrier.bmp");        
        }

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
        /// <param name="ID">ImageStore.Image.ID</param>
        /// <param name="positionZ">Z Position</param>
        /// <param name="x1">Oben Links</param>
        /// <param name="y1">Oben Links</param>
        /// <param name="x2">Oben Rechts</param>
        /// <param name="y2">Oben Rechts</param>
        /// <param name="x3">Unten Rechts</param>
        /// <param name="y3">Unten Rechts</param>
        /// <param name="x4">Unten Links</param>
        /// <param name="y4">Unten Links</param>
        public static void Texture(int ID, int positionZ, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();

            GL.Ortho(0, Screen.Width, Screen.Height, 0, -1, 1);

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

            GL.Ortho(0, Screen.Width, Screen.Height, 0, -1, 1);

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
        }
    }

    /// <summary>
    /// Hauptklasse des Spiels.
    /// </summary>
    public class Window : GameWindow
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

                    int MaxSpace = Convert.ToInt16(Screen.Height * 0.66 - 2 * Min);
                        
                    var rnd = new Random();
                    int tempA = rnd.Next(0, MaxSpace);

                    A = Min + tempA;
                    C = Min + MaxSpace - tempA;
                }

                /// <summary>
                /// Kollissionsabfrage
                /// </summary>
                /// <param name="x_l">x links</param>
                /// <param name="x_r">x rechts</param>
                /// <param name="y_t">y oben</param>
                /// <param name="y_b">y unten</param>
                /// <returns></returns>
                internal bool Collission(int x_l, int x_r, int y_t, int y_b)
                {
                    return
                        (y_t < A && x_l < _X && x_r > _X) ||
                        (y_b > Screen.Height - C && x_l < _X && x_r > _X);
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
                    Game.Draw.Texture(ImageStore.Barrier.ID, 2, _X - 12, 0, _X + 12, 0, _X + 12, A, _X - 12, A);

                    int yTop = Convert.ToInt16(Screen.Height - C);
                    int yBottom = Convert.ToInt16(Screen.Height);
                    Game.Draw.Texture(ImageStore.Barrier.ID, 1,
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
                Add();
                _Barriers = _Barriers.Where((item, index) => index > 2).ToArray();
            }

            // Punkte
            internal int Points = 0;

            // Liste der Barrieren
            Barrier[] _Barriers = new Barrier[0];

            // Distanz der Barrieren voneinander
            const int BarrierDistance = 250;

            /// <summary>
            /// Überprüft ob die Spielfigur mit einer Barriere Kollidiert
            /// </summary>
            /// <param name="a">x left</param>
            /// <param name="b">x right</param>
            /// <param name="c">y top</param>
            /// <param name="d">y bottom</param>
            /// <returns>Wahrheitswert ob eine Kollission zustande kam</returns>
            internal bool AnyCollision(int x_l, int x_r, int y_t, int y_b)
            {
                return _Barriers.Any(barrier => barrier.Collission(x_l, x_r, y_t, y_b));
            }

            /// <summary>
            /// Methode die für das Verschieben von allen Barrieren zuständig ist und löscht Barrieren die außerhalb des Bildschirms sind (x < 0)
            /// </summary>
            internal void Update()
            {
                _Barriers = _Barriers.Where(x => !AddPoint(x.Move(2))).ToArray();

                Add();
            }

            bool AddPoint(bool delete)
            {
                if (delete)
                    Points++;

                return delete;
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
                var tempBarriers = new List<Barrier>();

                while (((tempBarriers.Count + _Barriers.Count()) * BarrierDistance) < Screen.Width)
                    tempBarriers.Add(new Barrier(
                        tempBarriers.Count > 0 ?
                            tempBarriers.Last().X + BarrierDistance : (
                            _Barriers.Count() > 0 ?
                                _Barriers.Last().X + BarrierDistance :
                                BarrierDistance)));
                
                if (tempBarriers.Count == 0) return;

                var tempBarriersArray = tempBarriers.ToArray();
                int aOL = _Barriers.Length;
                Array.Resize(ref _Barriers, aOL + tempBarriersArray.Length);
                Array.Copy(tempBarriersArray, 0, _Barriers, aOL, tempBarriersArray.Length);
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
            static int X = Convert.ToInt16(Screen.Width / 3.0);

            public int X_L = X - ImageStore.Flabbypird.Width / 2;
            public int X_R = X + ImageStore.Flabbypird.Width / 2;
            public int Y_T = Convert.ToInt16(Screen.Height / 2) - ImageStore.Flabbypird.Height / 2;
            public int Y_B = Convert.ToInt16(Screen.Height / 2) + ImageStore.Flabbypird.Height / 2;

            /// <summary>
            /// Methode welche die vier Y Koordinaten der Spielfigur bewegt.
            /// </summary>
            /// <param name="moveY"></param>
            internal void Move(int moveY)
            {
                Y_T += moveY;
                Y_B += moveY;
            }

            // Gravitation Hoch wenn man die Leertaste drückt, und automatische Fallgravitation.
            const double GravitationUp = 140.0 / 60;
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
                Game.Draw.Image(ImageStore.Flabbypird, 0, X_L, Y_T);  
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
                int xMax = Convert.ToInt16(Screen.Width);
                int yMax = Convert.ToInt16(Screen.Height);
                Game.Draw.Texture(
                    ImageStore.Background.ID, 3,
                    0, 0,
                    xMax, 0,
                    xMax, yMax,
                    0, yMax
                    );
            }
        }
        #endregion

        #region Game

        Player _Player;
        Barriers _Barriers;
        Background _Background;

        bool GameStarted = false;

        // Aufrufe die Sekunde (double)
        const double UPDATE_MS = 60.0;
        const double FRAMES_MS = 60.0;

        /// <summary>
        /// Initialisierungsmethode.
        /// </summary>
        public Window() : base(Convert.ToInt16(Screen.Width), Convert.ToInt16(Screen.Height))
        {
            ImageStore.Initialize();

            Title = "Flabbypird";

            Load += game_Load;
            Resize += game_Resize;
            UpdateFrame += game_UpdateFrame;
            RenderFrame += game_RenderFrame;

            Keyboard.KeyDown += Keyboard_KeyDown;
            Keyboard.KeyDown += Keyboard_KeyDown_StartGame;
            Keyboard.KeyUp += Keyboard_KeyUp;

            Run(UPDATE_MS, FRAMES_MS);
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

        #endregion

        #region Draw

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

        #endregion

        #region Update

        /// <summary>
        /// Methode die für das aktualisieren der Spiellogic zuständig ist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void game_UpdateFrame(object sender, FrameEventArgs e)
        {
            if (Keyboard[Key.Space])
            {
                UpdateFrame -= game_UpdateFrame;
                UpdateFrame += game_UpdateFrame_Started;
            }
        }

        void game_UpdateFrame_Started(object sender, FrameEventArgs e)
        {
            if (_Barriers.AnyCollision(_Player.X_L, _Player.X_R, _Player.Y_T, _Player.Y_B))
            {
                this.Close();
                if (Flabbypird.Highscore.I.MinScore() < _Barriers.Points)
                {
                    this.Close();

                    //System.Threading.Thread t = new System.Threading.Thread(
                    //    new System.Threading.ThreadStart(
                    //        () => System.Windows.Forms.Application.Run(
                    //            new Flabbypird.AddHighScoreForm(
                    //                _Barriers.Points
                    //                ))));
                    //t.Start();
                }
                else
                {
                    //System.Threading.Thread t = new System.Threading.Thread(
                    //    new System.Threading.ThreadStart(
                    //        () => System.Windows.Forms.Application.Run(
                    //            new Flabbypird.AddHighScoreFailedForm()
                    //            )));

                    //t.Join();
                }
            }
            else
            {
                _Player.Update();
                _Barriers.Update();
            }
        }

        #endregion
    }
}
