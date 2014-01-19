using System;
using System.Collections.Generic;
using Cookie2D.World.Object;

namespace Cookie2D.World.Managers
{
	public static class MapManager
	{
		private static Dictionary<string, TileMap> _maps = new Dictionary<string, TileMap>();
		private static string _localmap = "local"; //just temporary for testing

		public static TileMap GetMap (string uid)
		{
			TileMap result;
			if (_maps.TryGetValue(uid, out result))
			{
				return result;
			}
			return result;
		}

		public static Dictionary<string, TileMap> GetMaps
		{
			get { return _maps; }
		}

		public static TileMap GetLocalMap
		{
			get {
				TileMap result;
				if (_maps.TryGetValue (_localmap, out result)) {
					return result;
				}
				return result;
			}
		}

		public static void AddMap(TileMap map)
		{
			_maps.Add(map.UniqueIdentifier, map);
		}

		public static void SetLocalUID(string UniqueIdentifier)
		{
			_localmap = UniqueIdentifier;
		}

		public static void RemoveMap(string uid)
		{
			_maps.Remove(uid);
		}

		public static TileMap FindMap(string name)
		{
			foreach (TileMap mp in _maps.Values)
				if (mp.Name.ToString().Equals(name)) return mp;
			return null;
		}
	}
}

