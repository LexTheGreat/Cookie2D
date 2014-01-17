using SFML.Graphics;
using SFML.Window;


namespace Cookie2D
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
            window.Clear();
            window.Display();
            window.SetVerticalSyncEnabled(VerticalSync);
            window.SetFramerateLimit((uint)FramerateLimit);
            return window;
        }
    }
}
