using SFML.Graphics;
using SFML.Window;
using CookieLib.Content;
using Cookie2D.World.Managers;

namespace Cookie2D.World.Entity
{
	public class Player : IEntity
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

		public bool CanMove()
		{
			IntRect tmprect;

			return true;
		}

		public override void Update()
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

				if (CanMove()) { Pos = tempPos; }
				Sprite.Position = Pos;
				FloatRect NameSize = Name.GetLocalBounds ();
				Name.Position = new Vector2f (Pos.X + 16 - NameSize.Width / 2, Pos.Y - 20);
			}
		}
	}
}
