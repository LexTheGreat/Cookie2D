/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using SFML.Graphics;
using SFML.Window;

namespace CookieLib
{
    public interface IDrawable
    {
		void Draw(RenderTarget renderWindow);
    }
}
