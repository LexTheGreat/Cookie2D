/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using SFML.Graphics;
using SFML.Window;
using System.IO;

namespace CookieLib
{
    public class GameSettings
    {
        public uint Width
        {
            get { return videoMode.Width; }
            set { videoMode.Width = value; }
        }
        public uint Height
        {
            get { return videoMode.Height; }
            set { videoMode.Height = value; }
        }
        public uint BitsPerPixel
        {
            get { return videoMode.BitsPerPixel; }
            set { videoMode.BitsPerPixel = value; }
        }

        public string Title = "Game";
        public Styles Style = Styles.Default;
		public Stream Icon = null;

        ContextSettings context = new ContextSettings(32, 0, 4);

        private VideoMode videoMode = new VideoMode(800, 600);

        public bool VerticalSync = true;
        public int FramerateLimit;

        public uint AntialiasingLevel
        {
            get { return context.AntialiasingLevel; }
            set { context.AntialiasingLevel = value; }
        }
        public uint DepthBits
        {
            get { return context.DepthBits; }
            set { context.DepthBits = value; }
        }
        public uint MajorVersion
        {
            get { return context.MajorVersion; }
            set { context.MajorVersion = value; }
        }
        public uint MinorVersion
        {
            get { return context.MinorVersion; }
            set { context.StencilBits = value; }
        }
        public uint StencilBits
        {
            get { return context.AntialiasingLevel; }
            set { context.AntialiasingLevel = value; }
        }

        public RenderWindow Create()
        {
            var window = new RenderWindow(videoMode, Title, Style, context);

            if (Icon != null) {
                using (var memoryStream = new MemoryStream())
                {
                    Icon.CopyTo(memoryStream);
                    window.SetIcon(32, 32, memoryStream.ToArray());
                }
            }
            window.SetVerticalSyncEnabled(VerticalSync);
            window.SetFramerateLimit((uint)FramerateLimit);
            return window;
        }
    }
}
