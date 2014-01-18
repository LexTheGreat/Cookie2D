using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace CookieLib.Content
{
    public static class ContentManager
    {
        static private readonly List<ContentProvider> loaders = new List<ContentProvider>();

        public static string Directory = "Content";

        static ContentManager()
        {         
            ContentProvider loader;
            loader = new ContentProvider();
            loader.Type = typeof (Texture);
            loader.Folder = "textures";
            loader.Extension = "png";
            loader.Load = (str) => new Texture(str);
            loader.Reuse = true;
            loaders.Add(loader);

            loader = new ContentProvider();
            loader.Type = typeof (Shader);
            loader.Folder = "shaders";
            loader.Extension = "glsl";
            loader.Load = (str) =>
                {
                    string source = File.ReadAllText(str);
                    return Shader.FromString(null, source);
                };
            loader.Reuse = false;
            loaders.Add(loader);

            loader = new ContentProvider();
            loader.Type = typeof (Font);
            loader.Folder = "fonts";
            loader.Extension = "ttf";
            loader.Load = (str) =>
                {
                    return new Font(str);
                };
            loader.Reuse = true;
            loaders.Add(loader);
        }

        public static T Load<T>(string path) where T : class
        {
            ContentProvider loader = loaders.First(x => x.Type == typeof (T));
            if (loader.Extension != null)
                path = String.Format("{0}/{1}/{2}.{3}", Directory, loader.Folder, path, loader.Extension);
            return loader.Get(path) as T;
        }

        public static void AddLoader(ContentProvider loader)
        {
            loaders.Add(loader);
        }
    }
}