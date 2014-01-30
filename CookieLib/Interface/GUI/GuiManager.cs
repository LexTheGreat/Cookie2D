/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieLib.Interface.GUI
{
    public static class GuiManager
    {
        static private readonly Dictionary<string, object> assets = new Dictionary<string, object>();

        public static T Get<T>(string name) where T : class
        {
            object result;
            if (assets.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return (T)result;
        }

        public static T Set<T>(string name, object obj) where T : class
        {
            if (!assets.ContainsKey(name))
                assets.Add(name, (T)obj);
			return (T)obj;
        }

		public static void Clear()
		{
			assets.Clear();
		}
    }
}
