using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using CookieLib;
using CookieLib.Graphics;
using NetEXT.TimeFunctions;

namespace CookieLib
{
    public class EntityList : IUpdateable, IDrawable, IDisposable
    {
        HashSet<IUpdateable> _updateables;
        HashSet<IDrawable> _drawables;
        HashSet<Drawable> _sfmlDrawables;

        public EntityList()
        {
            _updateables = new HashSet<IUpdateable>();
            _drawables = new HashSet<IDrawable>();
            _sfmlDrawables = new HashSet<Drawable>();
        }

        public void AddEntity<T>(T component)
        {
            IUpdateable updateable = component as IUpdateable;
            IDrawable drawable = component as IDrawable;
            ILoadable loadable = component as ILoadable;
            Drawable sfmlDrawable = component as Drawable;

            if (sfmlDrawable != null)
                _sfmlDrawables.Add(sfmlDrawable);

            if (updateable != null)
                _updateables.Add(updateable);

            if (drawable != null)
                _drawables.Add(drawable);

            //Add to the lists and then load it if required
            if (loadable != null)
                loadable.LoadContent();
        }

        public void RemoveEntity<T>(T component)
        {
            //We don't need to check for the component type here, just remove(the list will ignore it if it isn't added to the list)
            _updateables.Remove(component as IUpdateable);
            _drawables.Remove(component as IDrawable);
            _sfmlDrawables.Remove(component as Drawable);

            IDisposable disposable = component as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

		public void Update(Time deltaTime)
        {
            foreach (IUpdateable updateable in _updateables)
            {
				updateable.Update(deltaTime);
            }
        }

		public void Draw(RenderTarget renderWindow)
        {
            foreach (IDrawable drawable in _drawables)
            {
                drawable.Draw(renderWindow);
            }

            foreach (Drawable drawable in _sfmlDrawables)
            {
                renderWindow.Draw(drawable);
            }
        }

        public void Dispose()
        {
            //Using a hashset to deal with duplicates, since you can add a entity to all lists, but it is only one object. Disposing something more than one time = bad.
            HashSet<IDisposable> toDispose = new HashSet<IDisposable>();

            foreach (IUpdateable updateable in _updateables)
            {
                if (updateable is IDisposable)
                    toDispose.Add(updateable as IDisposable);
            }

            foreach (IDrawable drawable in _drawables)
            {
                if (drawable is IDisposable)
                    toDispose.Add(drawable as IDisposable);
            }

            foreach (Drawable drawable in _sfmlDrawables)
            {
				if (drawable is IDisposable)
					toDispose.Add(drawable as IDisposable);
            }

            foreach (IDisposable disposable in toDispose)
            {
                disposable.Dispose();
            }

            _updateables.Clear();
            _drawables.Clear();
            _sfmlDrawables.Clear();
            toDispose.Clear();
        }
    }

}
