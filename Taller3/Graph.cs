using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
						var tag = Convert.ToSingle(lineWord[1]);
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
						var weight = Convert.ToSingle(lineWord[1]);
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

		public List<string> F0(string id)
		{
			NodeGraph[id].Active = true;

			return new List<string> { id };
		}

		public List<string> F1(string id)
		{
			var f1List = F0(id);

			var f0Node = NodeGraph[f1List[0]];

			foreach (var influencedConection in f0Node.InfluenceList.Where(influencedConection => influencedConection.Value >= NodeGraph[influencedConection.Key].Tag))
			{
				if (NodeGraph[influencedConection.Key].Active)
					continue;

				NodeGraph[influencedConection.Key].Active = true;
				f1List.Add(NodeGraph[influencedConection.Key].Id);
			}

			return f1List;
		}

		public List<string> F2(string id)
		{
			var f2List = F1(id);

			foreach (var activeNodeId in f2List)
			{
				foreach (var nodesToInfluence in NodeGraph[activeNodeId].InfluenceList)
				{
					var inspectingNode = NodeGraph[nodesToInfluence.Key];

					if (inspectingNode.Active)
						continue;
					
					var sumEnfluenceImpact = inspectingNode.InfluenceMeList.Where(influenceMe => !NodeGraph[influenceMe.Key].Active).Sum(influenceMe => influenceMe.Value);

					if(sumEnfluenceImpact < inspectingNode.Tag)
						continue;

					inspectingNode.Active = true;
					f2List.Add(inspectingNode.Id);
				}
			}

			OutPutList("F2LIST", f2List);
			return f2List;

		}

		public List<string> F3(string id)
		{
			var f3List = F2(id);

			foreach (var activeNodeId in f3List)
			{
				foreach (var nodesToInfluence in NodeGraph[activeNodeId].InfluenceList)
				{
					var inspectingNode = NodeGraph[nodesToInfluence.Key];

					if (inspectingNode.Active)
						continue;

					var sumEnfluenceImpact = inspectingNode.InfluenceMeList.Where(influenceMe => !NodeGraph[influenceMe.Key].Active).Sum(influenceMe => influenceMe.Value);

					if (sumEnfluenceImpact < inspectingNode.Tag)
						continue;

					inspectingNode.Active = true;
					f3List.Add(inspectingNode.Id);
				}
			}

			OutPutList("F3LIST", f3List);
			return f3List;
		}

		private void OutPutList(string title, List<string> ids)
		{
			Console.WriteLine();
			Console.WriteLine(title);
			var listString = ids.Aggregate("{", (current, id) => current + (id + ","));
			listString += "}";
			Console.WriteLine(listString);
		}
	}
}
