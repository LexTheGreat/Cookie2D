using System;
using System.Runtime.InteropServices;

////////////////////////////////////////////////////////////
/// <summary>
/// Credits to LaurentGomila for this code.
/// https://github.com/LaurentGomila/SFML.Net/blob/master/src/Graphics/Color.cs
/// </summary>
////////////////////////////////////////////////////////////
namespace CookieLib.Utils
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Utility class for manipulating 32-bits RGBA colors
    /// </summary>
    ////////////////////////////////////////////////////////////
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorARGB
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the color from its red, green and blue components
        /// </summary>
        /// <param name="red">Red component</param>
        /// <param name="green">Green component</param>
        /// <param name="blue">Blue component</param>
        ////////////////////////////////////////////////////////////
        public ColorARGB(byte red, byte green, byte blue) :
            this(red, green, blue, 255)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the color from its red, green, blue and alpha components
        /// </summary>
        /// <param name="red">Red component</param>
        /// <param name="green">Green component</param>
        /// <param name="blue">Blue component</param>
        /// <param name="alpha">Alpha (transparency) component</param>
        ////////////////////////////////////////////////////////////
        public ColorARGB(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the color from another
        /// </summary>
        /// <param name="color">Color to copy</param>
        ////////////////////////////////////////////////////////////
        public ColorARGB(ColorARGB color) :
            this(color.R, color.G, color.B, color.A)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[Color]" +
                   " R(" + R + ")" +
                   " G(" + G + ")" +
                   " B(" + B + ")" +
                   " A(" + A + ")";
        }

        /// <summary>Red component of the color</summary>
        public byte R;

        /// <summary>Green component of the color</summary>
        public byte G;

        /// <summary>Blue component of the color</summary>
        public byte B;

        /// <summary>Alpha (transparent) component of the color</summary>
        public byte A;

        /// <summary>Predefined black color</summary>
        public static readonly ColorARGB Black = new ColorARGB(0, 0, 0);

        /// <summary>Predefined white color</summary>
        public static readonly ColorARGB White = new ColorARGB(255, 255, 255);

        /// <summary>Predefined red color</summary>
        public static readonly ColorARGB Red = new ColorARGB(255, 0, 0);

        /// <summary>Predefined green color</summary>
        public static readonly ColorARGB Green = new ColorARGB(0, 255, 0);

        /// <summary>Predefined blue color</summary>
        public static readonly ColorARGB Blue = new ColorARGB(0, 0, 255);

        /// <summary>Predefined yellow color</summary>
        public static readonly ColorARGB Yellow = new ColorARGB(255, 255, 0);

        /// <summary>Predefined magenta color</summary>
        public static readonly ColorARGB Magenta = new ColorARGB(255, 0, 255);

        /// <summary>Predefined cyan color</summary>
        public static readonly ColorARGB Cyan = new ColorARGB(0, 255, 255);

        /// <summary>Predefined (black) transparent color</summary>
        public static readonly ColorARGB Transparent = new ColorARGB(0, 0, 0, 0);
    }
}