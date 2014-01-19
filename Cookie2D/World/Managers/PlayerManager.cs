using System;
using System.Collections.Generic;
using Cookie2D.World.Entity;

namespace Cookie2D.World.Managers
{
	public static class PlayerManager
	{
		private static Dictionary<string, Player> _players = new Dictionary<string, Player>();
		private static string _localindex = "local"; //just temporary for testing

		public static Player GetPlayer (string uid)
		{
			Player result;
			if (_players.TryGetValue(uid, out result))
			{
				return result;
			}
			return result;
		}

		public static Dictionary<string, Player> GetPlayers
		{
			get { return _players; }
		}

		public static Player GetLocalPlayer
		{
			get {
				Player result;
				if (_players.TryGetValue (_localindex, out result)) {
					return result;
				}
				return result;
			}
		}

		public static void AddPlayer(Player player)
		{
			_players.Add(player.UniqueIdentifier, player);
		}

		public static void SetLocalUID(string UniqueIdentifier)
		{
			_localindex = UniqueIdentifier;
		}

		public static void RemovePlayer(string uid)
		{
			_players.Remove(uid);
		}
		
		public static Player FindPlayer(string name)
		{
			foreach (Player ply in _players.Values)
				if (ply.Name.ToString().Equals(name)) return ply;
			return null;
		}
	}
}

