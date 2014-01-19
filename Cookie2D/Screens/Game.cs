using System;
using Gwen.Control;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using Cookie2D.World;
using Cookie2D.World.Entity;
using Cookie2D.World.Managers;
using CookieLib.Graphics;
using CookieLib.TileEngine;
using CookieLib.Content;
using CookieLib.Interface;
using Cookie2D.World.Object;

namespace Cookie2D.Screens
{
	public class GameScreen : ScreenProvider
	{
		public GameScreen (Vector2i CurrentScreenSize, string GuiImagePath) 
			: base(CurrentScreenSize, GuiImagePath) { }

		public override void ScreenActivated()
		{
		}
		
		public override void Draw(RenderTarget Target)
		{
			MapManager.GetLocalMap.GetRenderer.Draw(Target, RenderStates.Default);
			spriteBatch.Begin();
			foreach (Player ply in PlayerManager.GetPlayers.Values)
				spriteBatch.Draw(ply.Sprite);
			spriteBatch.End();
			spriteBatch.Draw(Target, RenderStates.Default);
			foreach (Player ply in PlayerManager.GetPlayers.Values)
				ply.Name.Draw(Target, RenderStates.Default);
		}

		public override void Update(Time DeltaTime) 
		{ 
			foreach (Player ply in PlayerManager.GetPlayers.Values)
				ply.Update ();
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

