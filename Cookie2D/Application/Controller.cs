using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;
using Gwen.Control;
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
using Cookie2D.Graphics;

namespace Cookie2D
{
    public abstract class Controller
	{
		#region Definitions
		private RenderWindow _gamewindow = null;
		private Canvas _gamegui = null;
		private Gwen.Input.SFML _input = null;
		private SpriteBatch _spritebatch = null;
		private Color _clearcolor = Color.Black;
		private bool _stop = false;
		#endregion

		public RenderWindow GameWindow
		{
			get
			{
				return _gamewindow;
			}
		}

		public Canvas GameGUI
		{
			get
			{
				return _gamegui;
			}
		}

		public SpriteBatch spriteBatch
		{
			get
			{
				return _spritebatch;
			}
		}

		public Gwen.Input.SFML GameInput
		{
			get
			{
				return _input;
			}
		}

		public Color ClearColor
		{
			get
			{
				return _clearcolor;
			}

			set
			{
				_clearcolor = value;
			}
		}

        public bool Stop
        {
            get
            {
				return _stop;
            }
            set
            {
				_stop = value;
            }
        }

        public void Run(GameSettings settings = null)
        {
			//Initialize game settings
            if(settings == null) settings = new GameSettings();
			_gamewindow = settings.Create();

			//Create sprite batch
			_spritebatch = new SpriteBatch();

            // Setup event handlers
			_gamewindow.Closed += window_Closed;
			_gamewindow.KeyPressed += window_KeyPressed;
			_gamewindow.Resized += window_Resized;
			_gamewindow.KeyReleased += window_KeyReleased;
			_gamewindow.MouseButtonPressed += window_MouseButtonPressed;
			_gamewindow.MouseButtonReleased += window_MouseButtonReleased;
			_gamewindow.MouseWheelMoved += window_MouseWheelMoved;
			_gamewindow.MouseMoved += window_MouseMoved;
			_gamewindow.TextEntered += window_TextEntered;

            // create GWEN renderer
			Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(_gamewindow);

            // Create GWEN skin
            //Skin.Simple skin = new Skin.Simple(GwenRenderer);
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Content/textures/gui/DefaultSkin.png");

            // set default font
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer) { Size = 15, FaceName = "Verdana" };

            skin.SetDefaultFont(defaultFont.FaceName, defaultFont.Size);
            defaultFont.Dispose(); // skin has its own

            // Create a Canvas (it's root, on which all other GWEN controls are created)
			_gamegui = new Canvas(skin);
			_gamegui.SetSize((int)settings.Width, (int)settings.Height);
			_gamegui.ShouldDrawBackground = false;
			_gamegui.KeyboardInputEnabled = true;

            // Create GWEN input processor
            _input = new Gwen.Input.SFML();
			_input.Initialize(_gamegui, _gamewindow);

            var watch = Stopwatch.StartNew();

			ScreenActivated();
			while (_gamewindow.IsOpen() && !_stop)
            {
                var time = (uint)watch.Elapsed.TotalMilliseconds;

                Update(time);

				_gamewindow.DispatchEvents();
				_gamewindow.Clear(ClearColor);
                // Clear depth buffer
                Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);

				Draw(_gamewindow);
				_gamegui.RenderCanvas();

				_gamewindow.Display();
            }
			ScreenDeactivated();

        }

		public void window_Closed(object sender, EventArgs e)
        {
			_gamewindow.Close();
        }
		
		public void window_Resized(object sender, SizeEventArgs e)
        {
			_gamewindow.Size = new Vector2u((uint)e.Width, (uint)e.Height);
			_gamegui.SetSize((int)e.Width, (int)e.Height);
        }

		public void window_TextEntered(object sender, TextEventArgs e)
        {
            _input.ProcessMessage(e);
        }

		public void window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            _input.ProcessMessage(e);
        }

		public void window_MouseMoved(object sender, MouseMoveEventArgs e)
		{
			_input.ProcessMessage(e);
			MouseMoved(sender, e);
		}

		public void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
			MouseButtonPressed(sender, e);
        }

		public void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            _input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
			MouseButtonReleased(sender, e);
        }

		public void window_KeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.Escape)
				_gamewindow.Close();

			_input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
			KeyPressed(sender,e);
		}

		public void window_KeyReleased(object sender, KeyEventArgs e)
        {
			_input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
            KeyReleased(sender, e);
        }

		#region Functions
		protected abstract void Draw(RenderTarget Target);
		protected virtual void ScreenActivated() { }
		protected virtual void ScreenDeactivated() { }
		protected virtual void Update(float dT) { }

        // Input Bindings
		protected virtual void KeyPressed(object sender, KeyEventArgs EventArgs) { }
		protected virtual void KeyReleased(object sender, KeyEventArgs EventArgs) { }
		protected virtual void MouseButtonPressed(object sender, MouseButtonEventArgs EventArgs) { }
		protected virtual void MouseButtonReleased(object sender, MouseButtonEventArgs EventArgs) { }
		protected virtual void MouseMoved(object sender, MouseMoveEventArgs EventArgs) { }
		#endregion
	}
}
