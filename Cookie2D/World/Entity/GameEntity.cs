using System;
using SFML.Graphics;
using SFML.Window;
using CookieLib;
using NetEXT.TimeFunctions;
using CookieLib.Graphics;

namespace Cookie2D.World.Entity
{
	public enum Direction
	{
		Up = 0,
		Down,
		Left,
		Right
	}

	/// <summary>
	/// Can be used for players, NPCs and many more...
	/// </summary>
	public abstract class GameEntity : IDrawable, IUpdateable
	{
		private string _uid = null;
		private Sprite _sprite = null;
		private Text _name = null;
		private Vector2f _position = new Vector2f (0, 0);
		private byte _dir = (byte)Direction.Down;
		public bool Moving { get; set;}
		public IntRect BoundingBox { get; set;} 

		public string UniqueIdentifier
		{
			get { return _uid; }
			set { _uid = value; }
		}

		public Text Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public Sprite Sprite
		{
			get { return _sprite; }
			set { _sprite = value; }
		}

		public Vector2f Pos
		{
			get { return _position; }
			set { _position = value; }
		}

		public byte Dir
		{
			get { return _dir; }
			set { _dir = value; }
		}

		public GameEntity(string UniqueIdentifier, Sprite sprite, Text name, Vector2f position)
		{
			_uid = UniqueIdentifier;
			_sprite = new Sprite (sprite);
			_position = position;
			_name = new Text (name);
		}

		public virtual void Draw (RenderTarget renderTarget) { }
		public virtual void Update (Time deltaTime) { }
	}
}

