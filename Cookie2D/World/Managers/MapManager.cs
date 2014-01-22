using System;
using System.Collections.Generic;
using CookieLib.Tiled;

namespace Cookie2D.World.Managers
{
	public static class MapManager
	{
		private static Dictionary<string, TmxMap> _maps = new Dictionary<string, TmxMap>();
		private static string _localmap = "local"; //just temporary for testing

		public static TmxMap GetMap (string uid)
		{
			TmxMap result;
			if (_maps.TryGetValue(uid, out result))
			{
				return result;
			}
			return result;
		}

		public static Dictionary<string, TmxMap> GetMaps
		{
			get { return _maps; }
		}

		public static TmxMap GetLocalMap
		{
			get {
				TmxMap result;
				if (_maps.TryGetValue (_localmap, out result)) {
					return result;
				}
				return result;
			}
		}

		public static void AddMap(string UniqueIdentifier, TmxMap map)
		{
			_maps.Add(UniqueIdentifier, map);
		}

		public static void SetLocalUID(string UniqueIdentifier)
		{
			_localmap = UniqueIdentifier;
		}

		public static void RemoveMap(string uid)
		{
			_maps.Remove(uid);
		}
		
	}
}

