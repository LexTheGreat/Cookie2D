/* File Description
 * Original Works/Author: krzat
 * Other Contributors: Thomas Slusny
 * Author Website: https://bitbucket.org/krzat/sfml.utils
 * License: GPL 3.0
*/

using System;
using System.Collections.Generic;

namespace CookieLib.Resources
{
    public class ContentProvider : IDisposable
    {
        private readonly Dictionary<string, object> assets = new Dictionary<string, object>();
        public string Extension;
        public string Folder;
        public Func<string, object> Load;
        public bool Reuse;
        public Type Type;

        public void Dispose()
        {
            if (!Type.IsAssignableFrom(typeof (IDisposable))) return;
            foreach (object o in assets.Values)
            {
                var disposable = o as IDisposable;
                disposable.Dispose();
            }
        }

        public virtual object Get(string name)
        {
            object result;
            if (Reuse && assets.TryGetValue(name, out result))
            {
                return result;
            }
            result = Load(name);
            if (Reuse)
                assets.Add(name, result);
            return result;
        }
    }
}