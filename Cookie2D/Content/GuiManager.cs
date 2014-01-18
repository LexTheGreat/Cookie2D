﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie2D.Content
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
            assets.Add(name, (T)obj);
            return (T)obj;
        }
    }
}