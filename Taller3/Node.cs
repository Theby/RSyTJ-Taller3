using System;
using System.Collections.Generic;

namespace Taller3
{
	class Node
	{
		public string Id;
		public int Tag;
		public bool Active;

		/// <summary>
		/// - (Node ID, Weight)
		/// </summary>
		public Dictionary<string, int> InfluenceList;
		public Dictionary<string, int> InfluenceMeList;

		public Node(string id, int tag)
		{
			Id = id;
			Tag = tag;
			Active = false;

			InfluenceList = new Dictionary<string, int>();
			InfluenceMeList = new Dictionary<string, int>();
		}
	}
}
