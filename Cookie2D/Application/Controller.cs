using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using Tao.OpenGl;
using Gwen.Control;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Cookie2D
{
    public abstract class Controller
    {
        public View Camera { get; private set; }
        public RenderWindow Window { get; private set; }
        public Color ClearColor = Color.Black;
        public Canvas GUI { get; private set; }
        protected Gwen.Input.SFML _input;
        private bool _stoploop = false;

        public bool Stop
        {
            get
            {
                return _stoploop;
            }
            set
            {
                _stoploop = value;
            }
        }

        public void Run(GameSettings settings = null)
        {
            if(settings == null) settings = new GameSettings();
            Window = settings.Create();

            // Setup event handlers
            Window.Closed += window_Closed;
            Window.KeyPressed += window_KeyPressed;
            Window.Resized += window_Resized;
            Window.KeyReleased += window_KeyReleased;
            Window.MouseButtonPressed += window_MouseButtonPressed;
            Window.MouseButtonReleased += window_MouseButtonReleased;
            Window.MouseWheelMoved += window_MouseWheelMoved;
            Window.MouseMoved += window_MouseMoved;
            Window.TextEntered += window_TextEntered;

            // create GWEN renderer
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(Window);

            // Create GWEN skin
            //Skin.Simple skin = new Skin.Simple(GwenRenderer);
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Content/textures/gui/DefaultSkin.png");

            // set default font
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer) { Size = 15, FaceName = "Verdana" };

            skin.SetDefaultFont(defaultFont.FaceName, defaultFont.Size);
            defaultFont.Dispose(); // skin has its own

            // Create a Canvas (it's root, on which all other GWEN controls are created)
            GUI = new Canvas(skin);
            GUI.SetSize((int)settings.Width, (int)settings.Height);
            GUI.ShouldDrawBackground = false;
            GUI.KeyboardInputEnabled = true;

            // Create GWEN input processor
            _input = new Gwen.Input.SFML();
            _input.Initialize(GUI, Window);

            Camera = new View(Window.DefaultView);

            var watch = Stopwatch.StartNew();

            Initialize();
            while (Window.IsOpen() && !_stoploop)
            {
                var time = (uint)watch.Elapsed.TotalMilliseconds;

                Update(time);

                Window.Clear(ClearColor);
                Window.SetView(Camera);
                // Clear depth buffer
                Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);

                Draw(time);
                GUI.RenderCanvas();

                Window.Display();
                Window.DispatchEvents();
            }
            Unload();

        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        void window_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        void window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                Window.Close();
            
            _input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
            OnKeyPressed(sender,e);
        }

        void window_Resized(object sender, SizeEventArgs e)
        {
            var size = new Vector2f(e.Width, e.Height);
            var cam = Camera.Size;

            var rX = cam.X/size.X;
            var rY = cam.Y/size.Y;

            cam = Math.Max(rX, rY) * size;

            Camera.Size = cam;
            Window.DefaultView.Size = size;
            Window.DefaultView.Center = size / 2;
            GUI.SetSize((int)e.Width, (int)e.Height);
            OnResized(sender, e);
        }

        void window_TextEntered(object sender, TextEventArgs e)
        {
            _input.ProcessMessage(e);
        }

        void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            _input.ProcessMessage(e);
        }

        void window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            _input.ProcessMessage(e);
        }

        void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
        }

        void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
        }

        void window_KeyReleased(object sender, KeyEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
            OnKeyReleased(sender, e);
        }

        protected abstract void Initialize();

        protected abstract void Draw(float dT);

        protected virtual void Update(float dT)
        {
        }

        protected virtual void Unload()
        {
        }

        // Input Bindings
        protected virtual void OnKeyPressed(object sender, KeyEventArgs e)
        {
        }

        protected virtual void OnResized(object sender, SizeEventArgs e)
        {
        }

        protected virtual void OnKeyReleased(object sender, KeyEventArgs e)
        {
        }
    }
}
