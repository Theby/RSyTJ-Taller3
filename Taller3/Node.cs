using System.Collections.Generic;

namespace Taller3
{
	public class Node
	{
		public string Id;
		public float Tag;
		public bool Active;

		/// <summary>
		/// - (Node ID, Weight)
		/// </summary>
		public Dictionary<string, float> InfluenceList;
		public Dictionary<string, float> InfluenceMeList;

		public Node(string id, float tag)
		{
			Id = id;
			Tag = tag;
			Active = false;

			InfluenceList = new Dictionary<string, float>();
			InfluenceMeList = new Dictionary<string, float>();
		}
	}
}
