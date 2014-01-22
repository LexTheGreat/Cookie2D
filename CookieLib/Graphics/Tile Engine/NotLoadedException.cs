using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieLib.Graphics.TileEngine{
	class NotLoadedException : Exception {
		private string p;

		public NotLoadedException(string p)
		{
			// TODO: Complete member initialization
			this.p = p;
		}
	}
}
