using System;
using SFML.Window;
using SFML.Graphics;
using NetEXT.TimeFunctions;
using Gwen.Control;
using CookieLib.Graphics;

namespace CookieLib.Interface
{
	public abstract class ScreenProvider
	{
		#region Variables
		private Canvas _gamegui = null;
		private Gwen.Input.SFML _ginput = null;
		private Vector2i _screensize = new Vector2i(0, 0);
		private string _guipath = null;
		protected SpriteBatch spriteBatch = null;
		protected Windows.Console console = null;
		#endregion

		#region Properties
		public Canvas GameGUI
		{
			get
			{
				return _gamegui;
			}
		}

		public Vector2i ScreenSize
		{
			get
			{
				return _screensize;
			}
		}

		public string GuiImagePath
		{
			get
			{
				return _guipath;
			}
		}
		#endregion

		#region Events
		public event Action<ScreenProvider> SwitchScreen;
		public event Action CloseScreen;
		#endregion

		#region Constructors
		public ScreenProvider(Vector2i CurrentScreenSize, string GuiImagePath)
		{
			//initializes optimized sprite renderer
			spriteBatch = new SpriteBatch();

			//sets screen size
			_screensize = CurrentScreenSize;

			//sets path of gui image file
			_guipath = GuiImagePath;
		}
		public void InitializeGUI(RenderTarget window)
		{
			// create GWEN renderer
			Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(window);

			// Create GWEN skin
			//Skin.Simple skin = new Skin.Simple(GwenRenderer);
			Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, _guipath);

			// set default font
			Gwen.Font defaultFont = new Gwen.Font(gwenRenderer) { Size = 15, FaceName = "Verdana" };

			skin.SetDefaultFont(defaultFont.FaceName, defaultFont.Size);
			defaultFont.Dispose(); // skin has its own

			// Create a Canvas (it's root, on which all other GWEN controls are created)
			_gamegui = new Canvas(skin);
			_gamegui.SetSize(_screensize.X, ScreenSize.Y);
			_gamegui.ShouldDrawBackground = false;
			_gamegui.KeyboardInputEnabled = true;

			// Create GWEN input processor
			_ginput = new Gwen.Input.SFML();
			_ginput.Initialize(_gamegui, window);

			//Initialize console window
			console = new Windows.Console(_gamegui);
		}
		#endregion

		public void window_TextEntered(RenderWindow sender, TextEventArgs e)
		{
			_ginput.ProcessMessage(e);
		}

		public void window_MouseWheelMoved(RenderWindow sender, MouseWheelEventArgs e)
		{
			_ginput.ProcessMessage(e);
		}

		public void window_MouseMoved(RenderWindow sender, MouseMoveEventArgs e)
		{
			_ginput.ProcessMessage(e);
			MouseMoved(sender, e);
		}

		public void window_MouseButtonPressed(RenderWindow sender, MouseButtonEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
			MouseButtonPressed(sender, e);
		}

		public void window_MouseButtonReleased(RenderWindow sender, MouseButtonEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
			MouseButtonReleased(sender, e);
		}

		public void window_KeyPressed(RenderWindow sender, KeyEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
			KeyPressed(sender,e);
		}

		public void window_KeyReleased(RenderWindow sender, KeyEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
			KeyReleased(sender, e);
		}

		#region Functions
		//Input bindings
		protected virtual void KeyPressed(RenderWindow sender, KeyEventArgs EventArgs) { }
		protected virtual void KeyReleased(RenderWindow sender, KeyEventArgs EventArgs) { }
		protected virtual void MouseButtonPressed(RenderWindow sender, MouseButtonEventArgs EventArgs) { }
		protected virtual void MouseButtonReleased(RenderWindow sender, MouseButtonEventArgs EventArgs) { }
		protected virtual void MouseMoved(object sender, MouseMoveEventArgs EventArgs) { }

		//Internal functions
		protected void OnSwitchScreen(ScreenProvider NewScreen) { if (SwitchScreen != null) SwitchScreen(NewScreen); }
		protected void OnCloseScreen() { if (CloseScreen != null) CloseScreen(); }

		//General screen functions
		public virtual void ScreenActivated() { }
		public virtual void ScreenDeactivated() { }
		public virtual void Update(Time DeltaTime) { }
		public abstract void Draw(RenderTarget Target);
		#endregion
	}
}