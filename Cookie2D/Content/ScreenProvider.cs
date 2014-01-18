using System;
using SFML.Window;
using SFML.Graphics;
using NetEXT.TimeFunctions;
using Gwen.Control;

namespace Cookie2D.Content
{
	public abstract class ScreenProvider
	{
		#region Variables
		private Canvas _gamegui = null;
		private Gwen.Input.SFML _ginput = null;
		private Vector2i _screensize = new Vector2i(0, 0);
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
		#endregion

		#region Events
		public event Action<ScreenProvider> SwitchScreen;
		public event Action CloseScreen;
		#endregion

		#region Constructors
		public ScreenProvider(RenderTarget window, Vector2i CurrentScreenSize)
		{
			_screensize = CurrentScreenSize;

			// create GWEN renderer
			Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(window);

			// Create GWEN skin
			//Skin.Simple skin = new Skin.Simple(GwenRenderer);
			Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Content/textures/gui/DefaultSkin.png");

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
		}
		#endregion

		public void window_TextEntered(object sender, TextEventArgs e)
		{
			_ginput.ProcessMessage(e);
		}

		public void window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
		{
			_ginput.ProcessMessage(e);
		}

		public void window_MouseMoved(object sender, MouseMoveEventArgs e)
		{
			_ginput.ProcessMessage(e);
			MouseMoved(sender, e);
		}

		public void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
			MouseButtonPressed(sender, e);
		}

		public void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
			MouseButtonReleased(sender, e);
		}

		public void window_KeyPressed(object sender, KeyEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
			KeyPressed(sender,e);
		}

		public void window_KeyReleased(object sender, KeyEventArgs e)
		{
			_ginput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
			KeyReleased(sender, e);
		}

		#region Functions
		protected virtual void KeyPressed(object sender, KeyEventArgs EventArgs) { }
		protected virtual void KeyReleased(object sender, KeyEventArgs EventArgs) { }
		protected virtual void MouseButtonPressed(object sender, MouseButtonEventArgs EventArgs) { }
		protected virtual void MouseButtonReleased(object sender, MouseButtonEventArgs EventArgs) { }
		protected virtual void MouseMoved(object sender, MouseMoveEventArgs EventArgs) { }
		public virtual void ScreenActivated() { }
		public virtual void ScreenDeactivated() { }
		protected void OnSwitchScreen(ScreenProvider NewScreen) { if (SwitchScreen != null) SwitchScreen(NewScreen); }
		protected void OnCloseScreen() { if (CloseScreen != null) CloseScreen(); }
		public virtual void Update(Time DeltaTime) { }
		public abstract void Draw(RenderTarget Target);
		#endregion
	}
}