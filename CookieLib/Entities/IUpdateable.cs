using System;
using NetEXT.TimeFunctions;

namespace CookieLib
{
    public interface IUpdateable
    {
		void Update(Time deltaTime);
    }
}
