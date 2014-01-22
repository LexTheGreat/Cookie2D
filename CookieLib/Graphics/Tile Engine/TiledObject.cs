/// Tmx C# Loader for SFML
/// by Vinicius "Epiplon" Castanheira
/// 
/// This software is under the GPL 3.0
/// You should receive a copy of this license within the program
/// If not, check it at https://www.gnu.org/licenses/gpl-3.0.txt

using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace CookieLib.Graphics.TileEngine {
	public class TiledObject {

		public string Name { get; set; }  // The name of the object. An arbitrary string.
		public string Type { get; set; }  // The type of the object. An arbitrary string.
		public int X { get; set; }        // The x coordinate of the object in pixels.
		public int Y { get; set; }        // The y coordinate of the object in pixels.
		public int Width { get; set; }    // The width of the object in pixels.
		public int Height { get; set; }   // The height of the object in pixels.
		public int Gid {get; set; }				// Reference to a tile. Object is the size of a tile and have no width and height.
		public Dictionary<string, string> Properties { get; set; } // Properties of each object in Tiled

		public TiledObject() {
			Properties = new Dictionary<string, string>();
			Name = Type = "";
			X = Y = Width = Height = Gid = 0;
		}
	}
}
