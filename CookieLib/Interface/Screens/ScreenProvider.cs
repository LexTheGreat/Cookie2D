/* File Description
 * Original Works/Author: zsbzsb
 * Other Contributors: Thomas Slusny
 * Author Website: 
 * License: MIT
*/

using System;
using CookieLib.Graphics;
using CookieLib;
using SFML.Window;
using SFML.Graphics;
using NetEXT.TimeFunctions;
using Gwen.Control;

namespace CookieLib.Interface.Screens
{
	public abstract class ScreenProvider : IDisposable
	{
		#region Variables
		private Canvas _gamegui = null;
		private RenderTarget _target = null;
		private Gwen.Input.SFML _ginput = null;
		private Vector2i _screensize = new Vector2i(0, 0);
		private string _guipath = null;
		private EntityList _entities = null;
		private Windows.Console _console = null;
		protected SpriteBatch spriteBatch = null;
		#endregion

		#region Properties
		public Windows.Console Console
		{
			get { return _console; }
		}

		public Canvas GameGUI
		{
			get { return _gamegui; }
		}

		public RenderTarget renderTarget
		{
			get { return _target; }
			set { _target = value; }
		}

		public Vector2i ScreenSize
		{
			get { return _screensize; }
		}

		public string GuiImagePath
		{
			get { return _guipath; }
		}

		public EntityList Entities
		{
			get { return _entities; }
		}
		#endregion

		#region Events
		public event Action<ScreenProvider> SwitchScreen;
		public event Action CloseScreen;
		#endregion

		#region Constructors
		public ScreenProvider(Vector2i CurrentScreenSize, string GuiImagePath)
		{
			//sets screen size
			_screensize = CurrentScreenSize;

			//sets path of gui image file
			_guipath = GuiImagePath;

			//initializes entity list
			_entities = new EntityList ();
		}

		public void LoadInterface()
		{
			// create GWEN renderer
			Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(_target);

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
			_ginput.Initialize(_gamegui, _target);

			//Initialize console window
			_console = new Windows.Console(_gamegui);
		}

		public void Dispose()
		{
			_gamegui.Dispose ();
			_gamegui = null;
			_ginput = null;
			_console.Dispose();
			_console = null;
			_target = null;
		}
		#endregion

		#region Input bindings
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
			if (e.Code == Keyboard.Key.LControl)
				_console.IsHidden = !_console.IsHidden;
			if (e.Code == Keyboard.Key.F12)
			{
				Image img = sender.Capture();
				if (img.Pixels == null)
				{
					_console.PrintText("Failed to capture window");
				}
				string path = String.Format("screenshot-{0:D2}{1:D2}{2:D2}.png", DateTime.Now.Hour, DateTime.Now.Minute,
					DateTime.Now.Second);
				_console.PrintText(path + " saved!");
				if (!img.SaveToFile(path))
					_console.PrintText("Failed to save screenshot");
				img.Dispose();
			}
			KeyPressed(sender,e);
		}

		public void window_KeyReleased(RenderWindow sender, KeyEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
			KeyReleased(sender, e);
		}
		#endregion

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
		public virtual void Draw(RenderTarget renderTarget) { }
		public virtual void Draw(RenderTarget renderTarget, SpriteBatch spriteBatch) { }
		#endregion
	}
}