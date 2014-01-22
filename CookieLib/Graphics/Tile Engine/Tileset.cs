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
	public class Tileset : IComparable	{
		/// <summary>The name of the tileset.</summary>
		public string Name { get; set; }
		/// <summary>The first GID, or the global number that represent the first tile in the tileset.</summary> 
		public int FirstGID {get; set;}
		/// <summary> Width of Tileset in tiles (ImageWidth/TileWidth)</summary>
		public int Width {get; set;}
		/// <summary> Height of Tileset in tiles (ImageHeight/TileHeight)</summary>
		public int Height {get; set;}
		/// <summary> Tile's width. </summary>
		public int TileWidth {get; set;}
		/// <summary> Tile's height. </summary>
		public int TileHeight {get; set;}
		/// <summary> The source directory of the image. </summary>
		public string ImageSource {get; set;}
		/// <summary> Image's width. </summary>
		public int ImageWidth {get; set;}
		/// <summary> Image's height. </summary>
		public int ImageHeight {get; set;}
		/// <summary> The Texture generated from ImageSFML. </summary>
		public Texture TextureSFML {get; set;}

		public Tileset() {
			Name = "";
			FirstGID = 0;
			Width = 0;
			Height = 0;
			TileWidth = 0;
			ImageSource = "";
			ImageWidth = 0;
			ImageHeight = 0;
		}

		public int CompareTo(object obj) {
			Tileset tset = (Tileset) obj;
			if (this.FirstGID > tset.FirstGID)
				return 1;
			if (this.FirstGID < tset.FirstGID)
				return -1;
			// else
			return 0;
		}
	}
}
