using System;
using System.Collections.Generic;
using System.IO;

namespace Taller3
{
	class Graph
	{
		/// <summary>
		/// (ID, Node)
		/// </summary>
		public Dictionary<string, Node> NodeGraph;

		public Graph()
		{
			NodeGraph = new Dictionary<string, Node>();
		}

		public bool PopulateFromFile(string fileName)
		{
			try
			{
				using (var sr = new StreamReader(fileName))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						var lineWord = line.Split(' ');

						var id = lineWord[0];
						var tag = Convert.ToInt32(lineWord[1]);
						var newNode = new Node(id, tag);

						NodeGraph.Add(id, newNode);
					}

					sr.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(">> The file could not be read: " + e.Message);
				return false;
			}

			return true;
		}

		public bool ConectNodesFromFile(string fileName)
		{
			try
			{
				using (var sr = new StreamReader(fileName))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						var lineWord = line.Split(' ');

						var fatherNodeid = lineWord[0];
						var weight = Convert.ToInt32(lineWord[1]);
						var childNodeid = lineWord[2];

						if (NodeGraph[fatherNodeid].InfluenceList.ContainsKey(childNodeid))
							continue;

						//Add Node that I influence
						NodeGraph[fatherNodeid].InfluenceList.Add(childNodeid, weight);

						if (NodeGraph[childNodeid].InfluenceMeList.ContainsKey(fatherNodeid))
							continue;

						//Add Node that influence me
						NodeGraph[childNodeid].InfluenceMeList.Add(fatherNodeid, weight);
					}

					sr.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(">> The file could not be read: " + e.Message);
				return false;
			}

			return true;
		}
	}
}
