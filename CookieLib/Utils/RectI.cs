using System;
namespace CookieLib.Utils
{
    [Serializable]
    public class RectI
    {
        private int _left;
        private int _top;
        private int _height;
        private int _width;

        public int Left
        {
            get { return _left; }
        }

        public int Top
        {
            get { return _top; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }

        public RectI(int left, int top, int height, int width)
        {
            _left = left;
            _top = top;
            _height = height;
            _width = width;
        }

        public RectI()
        {
            // TODO: Complete member initialization
        }
    }
}