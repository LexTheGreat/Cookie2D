using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;
using SFML.Utils;
using CookieLib;
using CookieLib.Interface;
using NetEXT.TimeFunctions;

namespace Cookie2D
{
    public static class Program
    {
        public const int screenX = 800;
        public const int screenY = 600;
		private static ScreenManager _screenmng;
		private static ScreenProvider _menu;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            /// Initialize game settings
            GameSettings _gameSettings = new GameSettings();
            _gameSettings.Width = screenX;
            _gameSettings.Height = screenY;
            _gameSettings.Title = "Cookie2D";
            _gameSettings.FramerateLimit = 64;
            _gameSettings.Style = Styles.Close;

            /// Initialize SFML window
			/// _game = new Cookie2D.Game();
			/// _game.ClearColor = new Color(100, 149, 237);
			/// _game.Run(_gameSettings);
			
			_screenmng = new ScreenManager (_gameSettings,
				new MenuScreen(new Vector2i(screenX, screenY),
					 "Content/textures/GUI/DefaultSkin.png"), 
				Time.Zero);
			_screenmng.RunLoop();
        } 
    }
}