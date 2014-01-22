﻿using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace CookieLib.Graphics.TileEngine {
	public class Layer {
		public string Name;
		public int Width { get; set; }
		public int Height { get; set; }
		public List<int> Data	{ get; set;} // The information on all the tiles
		public Dictionary<string, string> Properties { get; set; } // Properties of each object in Tiled
		public List<Sprite>Sprites	{ get; set;}

		public Layer() {
			Data = new List<int>();
			Sprites = new List<Sprite>();
			Properties = new Dictionary<string, string>();
		}
	}
}
