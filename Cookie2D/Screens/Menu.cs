using System;
using Gwen.Control;
using SFML.Window;
using SFML.Graphics;
using Cookie2D.World;
using Cookie2D.World.Entity;
using Cookie2D.World.Managers;
using CookieLib.Resources;
using CookieLib.Interface.Screens;
using CookieLib.Interface.GUI;
using CookieLib.Tiled;
using CookieLib.Utils;
using CookieLib.Graphics;

namespace Cookie2D.Screens
{
	public class MenuScreen : ScreenProvider
	{
		public MenuScreen(Vector2i CurrentScreenSize, string GuiImagePath) 
			: base(CurrentScreenSize, GuiImagePath) { }

		public override void ScreenActivated()
		{
			WindowControl lwnd = new WindowControl(GameGUI, "Login");
			lwnd.SetSize(400, 300);
			lwnd.SetPosition(200, 200);
			lwnd.DisableResizing();
			lwnd.Close();
			GuiManager.Set<WindowControl>("loginwindow", lwnd);

			Label lblLogin = new Label (lwnd);
			lblLogin.SetPosition (10, 35);
			lblLogin.Text = "Username:";

			TextBox txtLogin = new TextBox (lwnd);
			txtLogin.SetPosition (100, 30);
			txtLogin.SetSize (270, 30);
			txtLogin.SubmitPressed += txtLogin_SumbitPressed;
			GuiManager.Set<TextBox>("txtlogin", txtLogin);

			Button btnLoginAccept = new Button (lwnd);
			btnLoginAccept.SetSize (388, 40);
			btnLoginAccept.SetPosition (0, 225);
			btnLoginAccept.Text = "Enter realm";
			btnLoginAccept.SetImage("Content/textures/gui/buttons/login.png");
			btnLoginAccept.Clicked += btnLoginAccept_Clicked;

			Button btnLogin = new Button(GameGUI);
			btnLogin.Text = "Login";
			btnLogin.SetPosition(245, 550);
			btnLogin.SetSize(100, 40);
			btnLogin.SetImage("Content/textures/gui/buttons/login.png");
			btnLogin.Clicked += btnLogin_Clicked;

			Button btnReg = new Button(GameGUI);
			btnReg.Text = "Register";
			btnReg.SetPosition(btnLogin.X + btnLogin.Width + 5, btnLogin.Y);
			btnReg.SetSize(btnLogin.Width, btnLogin.Height);
			btnReg.SetImage("Content/textures/gui/buttons/register.png");

			Button btnCredits = new Button(GameGUI);
			btnCredits.Text = "Credits";
			btnCredits.SetPosition(btnReg.X + btnReg.Width + 5, btnLogin.Y);
			btnCredits.SetSize(btnLogin.Width, btnLogin.Height);
			btnCredits.SetImage("Content/textures/gui/buttons/credits.png");
		}

		public override void Draw(RenderTarget renderTarget,SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(ContentManager.Load<Texture>("gui/background"), new IntRect(0, 0, 800, 600), new Color(255, 255, 255));
			spriteBatch.Draw(ContentManager.Load<Texture>("gui/overlay"), new IntRect(0, 0, 800, 600), new Color(255, 255, 255));
			spriteBatch.Draw(ContentManager.Load<Texture>("gui/logo"), new Vector2f(160, 200), new Color(255, 255, 255));
			spriteBatch.End();
		}

		private void btnLogin_Clicked(Base control, EventArgs args)
		{
			WindowControl loginwind = GuiManager.Get<WindowControl>("loginwindow");
			if (loginwind.IsVisible)
				loginwind.Close();
			else
				loginwind.Show();
		}

		private void btnLoginAccept_Clicked(Base control, EventArgs args)
		{
			TextBox txtlogin = GuiManager.Get<TextBox>("txtlogin");
			JoinGame (txtlogin.Text);
		}

		private void txtLogin_SumbitPressed(Base control, EventArgs args)
		{
			TextBox box = control as TextBox;
			JoinGame (box.Text);
		}

		private void JoinGame(string pname)
		{
			Sprite _sprite = new Sprite (ContentManager.Load<Texture> ("sprites/hero1"));
			_sprite.Position = new Vector2f (0, 0);
			_sprite.TextureRect = new IntRect (0, 0, 32, 32);
			PlayerManager.AddPlayer(
				new Player("local",_sprite,
					new Text(pname,
						ContentManager.Load<Font>("DejaVuSans"),
						10),
					_sprite.Position));
			TmxMap _map = new TmxMap("Content/untitled.tmx");
			MapManager.SetLocalUID ("local");
			MapManager.AddMap ("local", _map);
			OnSwitchScreen(new GameScreen(new Vector2i(800,600), GuiImagePath));
		}
	}
}

