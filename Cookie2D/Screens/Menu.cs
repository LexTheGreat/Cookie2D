using System;
using Gwen.Control;
using SFML.Window;
using SFML.Graphics;
using Cookie2D.World;
using Cookie2D.World.Entity;
using Cookie2D.World.Managers;
using CookieLib.Content;
using CookieLib.Interface.Screens;
using CookieLib.Interface.GUI;
using CookieLib.Graphics.TileEngine;
using CookieLib.Utils;

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

		public override void Draw(RenderTarget Target)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(ContentManager.Load<Texture>("gui/background"), new FloatRect(0, 0, 800, 600), new Color(255, 255, 255));
			spriteBatch.Draw(ContentManager.Load<Texture>("gui/overlay"), new FloatRect(0, 0, 800, 600), new Color(255, 255, 255));
			spriteBatch.Draw(ContentManager.Load<Texture>("gui/logo"), new Vector2f(160, 200), new Color(255, 255, 255));
			spriteBatch.End();
			spriteBatch.Draw(Target, RenderStates.Default);
		}

		protected override void KeyPressed(RenderWindow sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.F12)
			{
				Image img = sender.Capture();
				if (img.Pixels == null)
				{
					console.PrintText("Failed to capture window");
				}
				string path = String.Format("screenshot-{0:D2}{1:D2}{2:D2}.png", DateTime.Now.Hour, DateTime.Now.Minute,
					DateTime.Now.Second);
					console.PrintText(path + " saved!");
				if (!img.SaveToFile(path))
					console.PrintText("Failed to save screenshot");
				img.Dispose();
			}
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
			Sprite psprite = new Sprite (ContentManager.Load<Texture> ("sprites/hero1"));
			psprite.Position = new Vector2f (0, 0);
			psprite.TextureRect = new IntRect (0, 0, 32, 32);
			PlayerManager.AddPlayer(
				new Player("local",psprite,
					new Text(pname,
						ContentManager.Load<Font>("DejaVuSans"),
						10),
					psprite.Position));
			Program.map = new Map ("Content/untitled.tmx");
			Program.map.loadTilesets();
			Program.map.loadLayers();
			Program.map.loadSprites ();
			OnSwitchScreen(new GameScreen(new Vector2i(800,600), GuiImagePath));
		}
	}
}
