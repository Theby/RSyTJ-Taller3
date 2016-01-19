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

						if (!NodeGraph[fatherNodeid].InfluenceList.ContainsKey(childNodeid))
						{
							NodeGraph[fatherNodeid].InfluenceList.Add(childNodeid, weight);
						}
						else
						{
							Console.WriteLine(string.Format("The node {0} has two connections to the child {1}. So the weigh conection {2} has been ignored.", fatherNodeid, childNodeid, weight));
						}
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
