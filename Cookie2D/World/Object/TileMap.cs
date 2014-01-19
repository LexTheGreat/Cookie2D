using System;
using System.Collections.Generic;
using CookieLib.TileEngine;
using SFML.Graphics;
using SFML.Window;

namespace Cookie2D.World.Object
{
	public class TileMap : IObject
	{
		private TileRenderer _tileEngine = null;
		private Tile[,] _tiles;
		private List<IntRect> _boundaries = new List<IntRect> ();

		public TileMap(string UniqueIdentifier, Sprite sprite, Text name, Tile[,] tiles) 
			: base(UniqueIdentifier, sprite, name) 
		{ 
			_tiles = tiles;
			TileProvider _tlprov  = (int x, int y, int layer, out Color color, out IntRect rec) 
			                        => {GetTile(x, y, layer, out color, out rec);};
			_tileEngine = new TileRenderer (sprite.Texture,
				_tlprov,
				32,
				1);
		}

		public void AddBoundary(IntRect rec)
		{
			_boundaries.Add (rec);
		}

		public void RemoveBoundary(IntRect rec)
		{
			_boundaries.Remove (rec);
		}

		public List<IntRect> GetBoundaries
		{
			get { return _boundaries; }
		}

		public TileRenderer GetRenderer
		{
			get { return _tileEngine; }
		}

		public Tile[,] GetTiles
		{
			get { return _tiles; }
		}

		private void GetTile(int x, int y, int layer, out Color color, out IntRect rec)
		{
			try
			{
				if (_tiles[x, y] != null)
				{
					color = _tiles[x, y].color;
					rec = _tiles[x, y].rec;
				}
				else
				{
					color = Color.Cyan;
					rec = new IntRect ();
				}
			}
			catch
			{
				color = Color.Cyan;
				rec = new IntRect ();
			}
		}
	}
}

