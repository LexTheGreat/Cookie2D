using System;
using SFML.Graphics;
using SFML.Window;

namespace Cookie2D.World.Object
{
	public abstract class IObject
	{
		private string _uid = null;
		private Sprite _sprite = null;
		private Text _name = null;

		public IObject ()
		{
		}

		public IObject(string UniqueIdentifier, Sprite sprite, Text name)
		{
			_uid = UniqueIdentifier;
			_sprite = new Sprite (sprite);
			_name = new Text (name);
		}

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
	}
}

