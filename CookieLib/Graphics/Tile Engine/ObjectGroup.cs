/// Tmx C# Loader for SFML
/// by Vinicius "Epiplon" Castanheira
/// 
/// This software is under the GPL 3.0
/// You should receive a copy of this license within the program
/// If not, check it at https://www.gnu.org/licenses/gpl-3.0.txt

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CookieLib.Graphics.TileEngine {
	public class ObjectGroup  : Collection<TiledObject> {
		public string Name;		// The name of the object group.
		public int X;					// The x coordinate of the object group in tiles. Defaults to 0 and can no longer be changed in Tiled Qt.
		public int Y;					// The y coordinate of the object group in tiles. Defaults to 0 and can no longer be changed in Tiled Qt.
		public int Width;			// The width of the object group in tiles. Meaningless.
		public int Height;		// The height of the object group in tiles. Meaningless.
		public Dictionary<string, object> Properties { get; set; } // Properties of the object layer
	}
}
