using System;
namespace CookieLib.Utils
{
    [Serializable]
    public class Vector2I
    {
        public Vector2I(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}