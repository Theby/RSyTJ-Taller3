using System;
using System.Collections.Generic;

namespace Taller3
{
	class Node
	{
		public string Id;
		public int Tag;

		/// <summary>
		/// - (Node ID, Weight)
		/// </summary>
		public Dictionary<string, int> InfluenceList;

		public Node(string id, int tag)
		{
			Id = id;
			Tag = tag;

			InfluenceList = new Dictionary<string, int>();
		}
	}
}
