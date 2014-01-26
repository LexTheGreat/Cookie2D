using System;
using Gwen.Control;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using Cookie2D.World;
using Cookie2D.World.Entity;
using Cookie2D.World.Managers;
using CookieLib.Interface.Screens;
using CookieLib.Tiled;
using CookieLib.Tiled.Renderer;
using CookieLib.Graphics;

namespace Cookie2D.Screens
{
	public class GameScreen : ScreenProvider
	{
		private TmxMosaic _maprenderer = null;
		public GameScreen (Vector2i CurrentScreenSize, string GuiImagePath) 
			: base(CurrentScreenSize, GuiImagePath) { }

		public override void ScreenActivated()
		{
			//Program.screenmng.Camera.Center = PlayerManager.GetLocalPlayer.Pos;
			_maprenderer = new TmxMosaic (renderTarget, MapManager.GetLocalMap);
		}

		public override void ScreenDeactivated()
		{
			//Program.screenmng.Camera.Center = new Vector2f (Program.screenX /2, Program.screenY /2);
		}


		public override void Draw(RenderTarget Target, SpriteBatch spriteBatch)
		{
			_maprenderer.DrawCanvas (spriteBatch);
			spriteBatch.Begin(RenderStates.Default);
				foreach (Player ply in PlayerManager.GetPlayers.Values)
				spriteBatch.Draw(ply.Sprite);
			spriteBatch.End();
			foreach (Player ply in PlayerManager.GetPlayers.Values)
				ply.Name.Draw(Target, RenderStates.Default);
		}

		public override void Update(Time deltaTime) 
		{ 
			foreach (Player ply in PlayerManager.GetPlayers.Values)
				ply.Update (deltaTime);
			_maprenderer.canvas.UpdateCamera (PlayerManager.GetLocalPlayer.Pos);
		}

		protected override void KeyPressed(RenderWindow sender, KeyEventArgs e)
		{
			switch (e.Code)
			{
			case Keyboard.Key.S:
				PlayerManager.GetLocalPlayer.Moving = true;
				PlayerManager.GetLocalPlayer.Dir = (byte)Direction.Down;
				PlayerManager.GetLocalPlayer.UpdateSprite ();
				break;
			case Keyboard.Key.W:
				PlayerManager.GetLocalPlayer.Moving = true;
				PlayerManager.GetLocalPlayer.Dir = (byte)Direction.Up;
				PlayerManager.GetLocalPlayer.UpdateSprite ();
				break;
			case Keyboard.Key.A:
				PlayerManager.GetLocalPlayer.Moving = true;
				PlayerManager.GetLocalPlayer.Dir = (byte)Direction.Left;
				PlayerManager.GetLocalPlayer.UpdateSprite ();
				break;
			case Keyboard.Key.D:
				PlayerManager.GetLocalPlayer.Moving = true;
				PlayerManager.GetLocalPlayer.Dir = (byte)Direction.Right;
				PlayerManager.GetLocalPlayer.UpdateSprite ();
				break;
			}

			if (e.Code == Keyboard.Key.Escape)
			{
				OnSwitchScreen(new MenuScreen(new Vector2i(800,600), GuiImagePath));
			}
		}

		protected override void KeyReleased(RenderWindow sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.S)
			{
				PlayerManager.GetLocalPlayer.Moving = false;
			}
			if (e.Code == Keyboard.Key.W)
			{
				PlayerManager.GetLocalPlayer.Moving = false;
			}
			if (e.Code == Keyboard.Key.A)
			{
				PlayerManager.GetLocalPlayer.Moving = false;
			}
			if (e.Code == Keyboard.Key.D)
			{
				PlayerManager.GetLocalPlayer.Moving = false;
			}
		}
	}
}

