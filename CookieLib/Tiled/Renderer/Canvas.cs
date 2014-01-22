/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
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

namespace CookieLib.Tiled.Renderer
{
	// Canvas tracks three coordinate systems:
	// 1. pWidth/Height: Canvas pixel count
	// 2. tWidth/Height: Canvas tile count
	public class TmxCanvas
	{
		// User-defined (or derived) fields
		public int tMaxWidth;
		public int tMaxHeight;

		// Canvas size
		public int pWidth;          // Canvas pixel width
		public int pHeight;         // Canvas pixel height
		public float tileScale;     // Canvas-to-window scale

		// Testing: tHalo = 0 (should be at least 2)
		public int tWidth;      // # of (full) tiles per width
		public int tHeight;     // # of (full) tiles per height
		public int tHalo = 0;   // # of rendered tiles outside viewport

		// Camera position (focal tile)
		public int pX;      // Camera X position in pixels
		public int pY;      // Camera Y position in pixels
		public int tX;      // Tile.X containing camera
		public int tY;      // Tile.Y containing camera

		// Canvas loop hoisting
		public int tStartX;
		public int tEndX;
		public int tStartY;
		public int tEndY;

		// Mosaic parameters
		// (Note: Testing with default arguments)
		public int pTileWidth = 32;      // Tile width in pixels
		public int pTileHeight = 32;     // Tile height in pixels

		// Necessary?
		RenderTarget game;
		public Vector2f camera;
		public Vector2f origin;

		public TmxCanvas(RenderTarget gameInput)
		{
			game = gameInput;
			RescaleCanvas();
		}

		public void RescaleCanvas()
		{
			// Screen pixel count
			var pWindowWidth = game.Size.X;
			var pWindowHeight = game.Size.Y;            

			// Testing
			// Define maximum tile width/height
			tMaxWidth = 30;
			tMaxHeight = 15;

			// Determine the minimum scaling
			var xScale = (float)pWindowWidth / (pTileWidth * tMaxWidth);
			var yScale = (float)pWindowHeight / (pTileHeight * tMaxHeight);
			tileScale = Math.Max(xScale, yScale);
			tWidth = (int)Math.Round(pWindowWidth / (pTileWidth * tileScale));
			tHeight = (int)Math.Round(pWindowHeight / (pTileHeight * tileScale));

			// Virtual height is prescribed (i.e. window-independent)
			pHeight = (int)Math.Round(pWindowHeight / tileScale);
			pWidth = (int)Math.Round(pWindowWidth / tileScale);

			//----
			// Testing (Normally pX,pY would be set externally)

			// Get centre pixel (or left/above centre)
			var pXc = (pWidth - 1) / 2;
			var pYc = (pHeight - 1) / 2;

			// Get tile index containing the pixel
			tX = pXc / pTileWidth;
			tY = pYc / pTileHeight;

			// Readjust pX, pY to the centre of the tile
			pX = tX * pTileWidth + pTileWidth / 2;
			pY = tY * pTileHeight + pTileHeight / 2;

//            Console.WriteLine("tX, tWidth: {0}, {1}", tX, tWidth);
			//--- End Testing

			// Visible tile range (tStart <= t < tEnd)
			tStartX = tX - (tWidth - 1) / 2 - tHalo;
			tEndX = tX + (tWidth - 1) / 2 + 1 + tHalo;
			tStartY = tY - (tHeight - 1) / 2 - tHalo;
			tEndY = tY + (tHeight - 1) / 2 + 1 + tHalo;

			camera = new Vector2f((float)pX, (float)pY);
			origin = camera - new Vector2f((float)(pWidth/2), (float)(pHeight/2));
		}

		public void UpdateViewport(object sender, EventArgs e)
		{
			RescaleCanvas();
		}
	}
}