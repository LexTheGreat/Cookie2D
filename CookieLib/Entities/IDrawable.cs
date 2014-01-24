using SFML.Graphics;
using SFML.Window;

namespace CookieLib
{
    public interface IDrawable
    {
		void Draw(RenderTarget renderWindow);
    }
}
