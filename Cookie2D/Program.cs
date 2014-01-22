using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using CookieLib;
using CookieLib.Interface.Screens;
using NetEXT.TimeFunctions;
using Cookie2D.Screens;
using Cookie2D.World;

namespace Cookie2D
{
    public static class Program
    {
        public const int screenX = 800;
        public const int screenY = 600;
		public static ScreenManager screenmng;

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

			
			screenmng = new ScreenManager (
				_gameSettings,
				new MenuScreen(new Vector2i(screenX, screenY),
					 "Content/textures/GUI/DefaultSkin.png"), 
				Time.FromMilliseconds(20));
			screenmng.RunLoop();
        } 
    }
}