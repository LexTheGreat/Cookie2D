using System;
using SFML.Graphics;
using SFML.Window;
using CookieLib.Content;

namespace CookieLib
{
    [Serializable]
	public abstract class IObject
	{

        private string _UID = null;
        private string _texturestring = null;
        private string _mapstring = null;
        [NonSerialized]
		private Sprite _sprite = null;
        [NonSerialized]
		private Text _name = null;

		public IObject() { }

        public IObject(string UID, string texture, string name)
		{
            _UID = UID;
            _texturestring = texture;
            _mapstring = name;
		}

        public void RenderInit()
        {
            _sprite = new Sprite(ContentManager.Load<Texture>("tiles/" + _texturestring));
            _name = new Text(_mapstring, ContentManager.Load<Font>("DejaVuSans"), 10);
        }

        public string UID
        {
            get { return _UID; }
            set { _UID = value; }
        }

        public string TextureString
        {
            get { return _texturestring; }
            set { _texturestring = value; }
        }

        public string MapString
        {
            get { return _mapstring; }
            set { _mapstring = value; }
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

