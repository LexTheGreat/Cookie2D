using SFML.Graphics;
using SFML.Window;
using CookieLib.Resources;
using Cookie2D.World.Managers;
using CookieLib;
using NetEXT.TimeFunctions;
namespace Cookie2D.World.Entity
{
	public class Player : GameEntity
	{
		public Player (string UniqueIdentifier, Sprite sprite, Text name, Vector2f position) 
			: base(UniqueIdentifier, sprite, name, position) { }

		public void UpdateSprite()
		{
			switch(Dir)
			{
			case (byte)Direction.Up:
				Sprite.TextureRect = new IntRect (96, 0, 32, 32);
				break;
			case (byte)Direction.Down:
				Sprite.TextureRect = new IntRect (32, 0, 32, 32);
				break;
			case (byte)Direction.Left:
				Sprite.TextureRect = new IntRect (64, 0, 32, 32);
				break;
			case (byte)Direction.Right:
				Sprite.TextureRect = new IntRect (0, 0, 32, 32);
				break;
			}
		}

		public bool CanMove(Vector2f temppos)
		{
			if (temppos.X < 0) return false;
			if (temppos.Y < 0) return false;
			if (temppos.X > (MapManager.GetLocalMap.Width - 1) * MapManager.GetLocalMap.TileWidth) return false;
			if (temppos.Y > (MapManager.GetLocalMap.Height -1) * MapManager.GetLocalMap.TileHeight) return false;
			return true;
		}

		public override void Update(Time deltaTime)
		{
			Vector2f tempPos;
			if (Moving)
			{
				switch (Dir) {
				case (byte)Direction.Down:
					tempPos = new Vector2f (Pos.X, Pos.Y + 1);
					break;
				case (byte)Direction.Up:
					tempPos = new Vector2f (Pos.X, Pos.Y - 1);
					break;
				case (byte)Direction.Left:
					tempPos = new Vector2f (Pos.X - 1, Pos.Y);
					break;
				case (byte)Direction.Right:
					tempPos = new Vector2f (Pos.X + 1, Pos.Y);
					break;
				default:
					tempPos = new Vector2f();
					break;
				}

				BoundingBox = new IntRect ((int)tempPos.X, (int)tempPos.Y + 16, 32, 16);

				if (CanMove(tempPos)) { 
					Pos = tempPos; 
					
				}
				Sprite.Position = Pos;
				FloatRect NameSize = Name.GetLocalBounds ();
				Name.Position = new Vector2f (Pos.X + 16 - NameSize.Width / 2, Pos.Y - 20);
			}
		}
	}
}

