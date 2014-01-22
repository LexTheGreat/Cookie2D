using System;
using System.Collections.Generic;
using CookieLib.Graphics.TileEngine;

namespace Cookie2D.World.Managers
{
	public static class MapManager
	{
		private static Dictionary<string, Map> _maps = new Dictionary<string, Map>();
		private static string _localmap = "local"; //just temporary for testing

		public static Map GetMap (string uid)
		{
			Map result;
			if (_maps.TryGetValue(uid, out result))
			{
				return result;
			}
			return result;
		}

		public static Dictionary<string, Map> GetMaps
		{
			get { return _maps; }
		}

		public static Map GetLocalMap
		{
			get {
				Map result;
				if (_maps.TryGetValue (_localmap, out result)) {
					return result;
				}
				return result;
			}
		}

		public static void AddMap(string UniqueIdentifier, Map map)
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

