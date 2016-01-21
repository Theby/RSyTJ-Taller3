using System.Collections.Generic;

namespace Taller3
{
	public class Node
	{
		public string Id;
		public bool Active;
		public float Tag;
		public int OTs;
		public int InfluenceSize;
		

		/// <summary>
		/// - (Node ID, Weight)
		/// </summary>
		public Dictionary<string, float> InfluenceList;
		public Dictionary<string, float> InfluenceMeList;

		public Node(string id, float tag, int ots)
		{
			Id = id;
			Tag = tag;
			Active = false;
			OTs = ots;
			InfluenceSize = 0;

			InfluenceList = new Dictionary<string, float>();
			InfluenceMeList = new Dictionary<string, float>();
		}
	}
}
