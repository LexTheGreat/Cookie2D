﻿/* File Description
 * Original Works/Author: krzat
 * Other Contributors: Thomas Slusny
 * Author Website: https://bitbucket.org/krzat/sfml.utils
 * License: GPL 3.0
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;


namespace CookieLib.Graphics
{
    /// <summary>
    /// Provides optimized drawing of sprites
    /// </summary>
	public class VertexBatch
    {
        private struct QueueItem
        {
            public uint Count;
            public Texture Texture;
        }

		private readonly List<QueueItem> _textures = new List<QueueItem>();
		private RenderTarget _renderTarget;
		private RenderStates _renderStates = RenderStates.Default;
		private Vertex[] vertices = new Vertex[100 * 4];
		private int count;
		private Texture activeTexture;
		private bool active;
		private uint queueCount;

		#region Variables

		public BlendMode BlendMode
		{
			get { return _renderStates.BlendMode; }
			set { _renderStates.BlendMode = value; }
		}

		public RenderTarget RenderTarget
		{
			get { return _renderTarget; }
			set { _renderTarget = value; }
		}

		#endregion

		#region Constructors

		public VertexBatch(RenderTarget graphicsDevice)
        {
			_renderTarget = graphicsDevice;
        }

		#endregion

		#region Functions

		public void Begin(BlendMode blendMode)
        {
            if (active) throw new Exception("Already active");
            count = 0;
			_textures.Clear();
			_renderStates.BlendMode = blendMode;
            active = true;
            activeTexture = null;
        }

        public void End()
        {
            if (!active) throw new Exception("Call end first.");
            Enqueue();
            active = false;

			uint index = 0;
			foreach (var item in _textures)
			{
				Debug.Assert(item.Count > 0);
				_renderStates.Texture = item.Texture;

				_renderTarget.Draw(vertices, index, item.Count, PrimitiveType.Quads, _renderStates);
				index += item.Count;
			}
        }

        private void Enqueue()
        {
            if (queueCount > 0)
				_textures.Add(new QueueItem
                {
                    Texture = activeTexture,
                    Count = queueCount
                });
            queueCount = 0;
        }

        private int Create(Texture texture)
        {
            if (!active) throw new Exception("Call Begin first.");

            if (texture != activeTexture)
            {
                Enqueue();
                activeTexture = texture;
            }

            if (count >= (vertices.Length / 4))
            {
				Array.Resize(ref vertices, vertices.Length * 2);
            }

            queueCount += 4;
            return 4 * count++;
        }

        public void Draw(IEnumerable<Sprite> sprites)
        {
            foreach (var s in sprites)
                Draw(s);
        }

        public void Draw(Sprite sprite)
        {
            Draw(sprite.Texture, sprite.Position, sprite.TextureRect, sprite.Color, sprite.Scale, sprite.Origin,
                 sprite.Rotation);
        }

        public unsafe void Draw(Texture texture, Vector2f position, IntRect rec, Color color, Vector2f scale,
                                Vector2f origin, float rotation = 0)
        {

            var index = Create(texture);
            float sin=0, cos=1;
            //FloatMath.SinCos(rotation, out sin, out cos);

            if (true)
            {
                rotation = CookieLib.Utils.FloatMath.ToRadians(rotation);
                sin = (float)Math.Sin(rotation);
                cos = (float)Math.Cos(rotation);
            }

            var pX = -origin.X * scale.X;
            var pY = -origin.Y * scale.Y;
            scale.X *= rec.Width;
            scale.Y *= rec.Height;

            fixed (Vertex* fptr = vertices)
            {
                var ptr = fptr + index;

                ptr->Position.X = pX * cos - pY * sin + position.X;
                ptr->Position.Y = pX * sin + pY * cos + position.Y;
                ptr->TexCoords.X = rec.Left;
                ptr->TexCoords.Y = rec.Top;
                ptr->Color = color;
                ptr++;

                pX += scale.X;
                ptr->Position.X = pX * cos - pY * sin + position.X;
                ptr->Position.Y = pX * sin + pY * cos + position.Y;
                ptr->TexCoords.X = rec.Left + rec.Width;
                ptr->TexCoords.Y = rec.Top;
                ptr->Color = color;
                ptr++;

                pY += scale.Y;
                ptr->Position.X = pX * cos - pY * sin + position.X;
                ptr->Position.Y = pX * sin + pY * cos + position.Y;
                ptr->TexCoords.X = rec.Left + rec.Width;
                ptr->TexCoords.Y = rec.Top + rec.Height;
                ptr->Color = color;
                ptr++;

                pX -= scale.X;
                ptr->Position.X = pX * cos - pY * sin + position.X;
                ptr->Position.Y = pX * sin + pY * cos + position.Y;
                ptr->TexCoords.X = rec.Left;
                ptr->TexCoords.Y = rec.Top + rec.Height;
                ptr->Color = color;
            }
        }

        public unsafe void Draw(Texture texture, FloatRect rec, IntRect src, Color color)
        {
            var index = Create(texture);

            fixed (Vertex* fptr = vertices)
            {
                var ptr = fptr + index;

                ptr->Position.X = rec.Left;
                ptr->Position.Y = rec.Top;
                ptr->TexCoords.X = src.Left;
                ptr->TexCoords.Y = src.Top;
                ptr->Color = color;
                ptr++;

                ptr->Position.X = rec.Left + rec.Width;
                ptr->Position.Y = rec.Top;
                ptr->TexCoords.X = src.Left + src.Width;
                ptr->TexCoords.Y = src.Top;
                ptr->Color = color;
                ptr++;

                ptr->Position.X = rec.Left + rec.Width;
                ptr->Position.Y = rec.Top + rec.Height;
                ptr->TexCoords.X = src.Left + src.Width;
                ptr->TexCoords.Y = src.Top + src.Height;
                ptr->Color = color;
                ptr++;

                ptr->Position.X = rec.Left;
                ptr->Position.Y = rec.Top + rec.Height;
                ptr->TexCoords.X = src.Left;
                ptr->TexCoords.Y = src.Top + src.Height;
                ptr->Color = color;
            }
        }

        public void Draw(Texture texture, FloatRect rec, Color color)
        {
            int width = 1, height = 1;
            if (texture != null)
            {
                width = (int)texture.Size.X;
                height = (int)texture.Size.Y;
            }
			Draw(texture, rec, new IntRect(0, 0, width, height), color);
        }

        public void Draw(Texture texture, Vector2f pos, Color color)
        {
            if (texture == null) throw new ArgumentNullException();
            var width = (int)texture.Size.X;
            var height = (int)texture.Size.Y;
            Draw(texture, new FloatRect(pos.X, pos.Y, width, height), new IntRect(0, 0, width, height), color);
        }

		#endregion
    }
}