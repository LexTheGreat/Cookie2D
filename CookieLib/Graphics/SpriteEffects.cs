/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using System;
using System.Linq;

namespace CookieLib.Graphics
{
	/// <summary>
	/// Enum of flags containing the effects that can be applied to a sprite when rendering it.
	/// </summary>
	[Flags]
	public enum SpriteEffects : byte
	{
		/// <summary>
		/// No effects specified.
		/// </summary>
		None = 0,

		/// <summary>
		/// Flips the sprite horizontally before rendering.
		/// </summary>
		FlipHorizontally = 1 << 0,

		/// <summary>
		/// Flips the sprite vertically before rendering.
		/// </summary>
		FlipVertically = 1 << 1,

		/// <summary>
		/// Flips the both vertically and horizontally before rendering.
		/// </summary>
		FlipVerticalHorizontal = FlipVertically | FlipHorizontally,
	}
}

