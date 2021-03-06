/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: Marshall Ward
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

using SFML.Graphics;
using SFML.Window;
using CookieLib.Tiled;
using CookieLib.Graphics;

namespace CookieLib.Tiled.Renderer
{
	public class TmxMosaic
	{
		public Dictionary<TmxTileset, Texture> spriteSheet;
		public Dictionary<int, IntRect> tileRect;
		public Dictionary<int, TmxTileset> idSheet;
		public List<int[,]> layerID;     // layerID[x,y]

		public int tMapWidth, tMapHeight;

		// Temporary
		public TmxMap map;          // TMX data (try to remove this)
		public TmxCanvas canvas;       // Viewport details

		public RenderTarget game;
		public SpriteBatch batch;

		public TmxMosaic(RenderTarget gameInput, TmxMap mapName)
		{
			// Temporary code
			game = gameInput;
			map = mapName;
			tMapWidth = map.Width;
			tMapHeight = map.Height;

			// Initialize graphics buffers
			canvas = new TmxCanvas(game, 
				new Vector2i(tMapWidth /mapName.TileWidth, tMapHeight/mapName.TileHeight), 
				new Vector2i(mapName.TileWidth, mapName.TileHeight));

			// Load spritesheets
			spriteSheet = new Dictionary<TmxTileset, Texture>();
			tileRect = new Dictionary<int, IntRect>();
			idSheet = new Dictionary<int, TmxTileset>();

			foreach (TmxTileset ts in map.Tilesets)
			{
				var newSheet = GetSpriteSheet(ts.Image.Source);
				spriteSheet.Add(ts, newSheet);

				// Loop hoisting
				var wStart = ts.Margin;
				var wInc = ts.TileWidth + ts.Spacing;
				var wEnd = ts.Image.Width;

				var hStart = ts.Margin;
				var hInc = ts.TileHeight + ts.Spacing;
				var hEnd = ts.Image.Height;

				// Pre-compute tileset rectangles
				var id = ts.FirstGid;
				for (var h = hStart; h < hEnd; h += hInc)
				{
					for (var w = wStart; w < wEnd; w += wInc)
					{
						var rect = new IntRect(w, h,
							ts.TileWidth, ts.TileHeight);
						idSheet.Add(id, ts);
						tileRect.Add(id, rect);
						id += 1;
					}
				}

				// Ignore properties for now
			}

			// Load id maps
			layerID = new List<int[,]>();
			foreach (TmxLayer layer in map.Layers)
			{
				var idMap = new int[tMapWidth, tMapHeight];
				foreach (TmxLayerTile t in layer.Tiles)
				{
					idMap[t.X, t.Y] = t.Gid;
				}
				layerID.Add(idMap);

				// Ignore properties for now
			}
		}

		public void DrawCanvas(SpriteBatch batch)
		{
			// Loop hoisting (Determined from Canvas)
			var iStart = Math.Max(0, canvas.tStartX);
			var iEnd = Math.Min(tMapWidth, canvas.tEndX);

			var jStart = Math.Max(0, canvas.tStartY);
			var jEnd = Math.Min(tMapHeight, canvas.tEndY);

			// Initialize the renderTarget spriteBatch
			batch.Begin();
			// Draw tiles inside canvas
			foreach (var idMap in layerID)
			{
				for (var i = iStart; i < iEnd; i++)
				{
					for (var j = jStart; j < jEnd; j++)
					{
						var id = idMap[i,j];

						// Skip unmapped cells
						if (id == 0) continue;

						// Pre-calculate? (not with tileScale in there...)
						var position = new Vector2f(
							map.TileWidth * canvas.tileScale * i,
							map.TileHeight * canvas.tileScale * j);
						batch.Draw(spriteSheet[idSheet[id]], position, tileRect[id], Color.White, 0.0f, canvas.origin, new Vector2f(canvas.tileScale ,canvas.tileScale));

					}
				}
			}
			batch.End();
		}

		public Texture GetSpriteSheet(string filepath)
		{
			Texture newSheet;
			newSheet = new Texture(filepath);
			return newSheet;
		}
	}
}