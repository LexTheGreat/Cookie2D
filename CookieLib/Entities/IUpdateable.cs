/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using System;
using NetEXT.TimeFunctions;

namespace CookieLib
{
    public interface IUpdateable
    {
		void Update(Time deltaTime);
    }
}
