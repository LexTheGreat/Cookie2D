using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cookie2D.Graphics;
using Cookie2D.Content;
using SFML.Graphics;
using SFML.Window;
using Gwen.Control;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Cookie2D
{
    public class Game: Controller
    {
        protected Windows.Console _console;
        

		protected override void ScreenActivated()
        {
			WindowControl lwnd = new WindowControl(GameGUI, "Login");
            lwnd.SetSize(400, 300);
            lwnd.SetPosition(200, 200);
            lwnd.DisableResizing();
            lwnd.Close();
            GuiManager.Set<WindowControl>("loginwindow", lwnd);

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

			_console = new Windows.Console(GameGUI);
        }

		protected override void Draw(RenderTarget Target)
        {
			spriteBatch.Begin();
            spriteBatch.Draw(ContentManager.Load<Texture>("gui/background"), new FloatRect(0, 0, 800, 600), new Color(255, 255, 255));
            spriteBatch.Draw(ContentManager.Load<Texture>("gui/overlay"), new FloatRect(0, 0, 800, 600), new Color(255, 255, 255));
            spriteBatch.Draw(ContentManager.Load<Texture>("gui/logo"), new Vector2f(160, 200), new Color(255, 255, 255));
            spriteBatch.End();
			spriteBatch.Draw(Target, RenderStates.Default);
        }

		protected override void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LControl)
                _console.IsHidden = !_console.IsHidden;

            if (e.Code == Keyboard.Key.F12)
            {
				Image img = GameWindow.Capture();
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
        }

        protected void btnLogin_Clicked(Base control, EventArgs args)
        {
            WindowControl loginwind = GuiManager.Get<WindowControl>("loginwindow");
            if (loginwind.IsVisible)
                loginwind.Close();
            else
                loginwind.Show();
        }
    }
}
