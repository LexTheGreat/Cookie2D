using System;
using SFML.Graphics;
using SFML.Window;

namespace CookieLib.TileEngine
{
	public class Tile
	{
		public Color color { get; set;}
		public IntRect rec { get; set;}
		public int layer { get; set;}

		public Tile()
		{
			color = Color.White;
			rec = new IntRect();
			layer = 1;
		}
	}
}