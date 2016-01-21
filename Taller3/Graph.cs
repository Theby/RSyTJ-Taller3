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

		public string[] FileNames;

		private readonly string _assemblyPath;

		public Graph()
		{
			NodeGraph = new Dictionary<string, Node>();
			_assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			FileNames = new []
			{
				"CentralidadInfluenciaF2(resumen).txt",
				"CentralidadInfluenciaF3(resumen).txt",
				"RetwittImpact(resumen).txt"
			};

			foreach (var file in FileNames.Where(File.Exists))
			{
				File.Delete(file);
			}
		}

		#region Centrality Influence

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

			NodeGraph[id].InfluenceSize = f1List.Count;
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
			NodeGraph[id].InfluenceSize = f2List.Count;

			//OutPutList("F2LIST", f2List);
			OutPutFnFile("F2", id, f2List);
			OutPutFnSummaryFile("F2", id);
			return f2List;

		}

		public void F3(string id)
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
			NodeGraph[id].InfluenceSize = f3List.Count;

			//OutPutList("F3LIST", f3List);
			OutPutFnFile("F3", id, f3List);
			OutPutFnSummaryFile("F3", id);
		}

		public void RetweetImpact(string id)
		{
			var impact = NodeGraph[id].OTs*Math.Log10(NodeGraph[id].InfluenceList.Count);

			OutPutRetweetsFile(id, impact);
		}

		#endregion

		#region Read From File

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

						var ots = 0;

						if (lineWord.Length == 3)
						{
							ots = Convert.ToInt32(lineWord[2]);
						}

						var newNode = new Node(id, tag, ots);

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

		#endregion

		#region Output to File

		private void OutPutList(string title, List<string> ids)
		{
			Console.WriteLine();
			Console.WriteLine(title);
			var listString = ids.Aggregate("{", (current, id) => current + (id + ","));
			listString += "}";
			Console.WriteLine(listString);
		}

		private void OutPutFnFile(string title, string fatherId, List<string> ids)
		{
			var fileName = $"CentralidadInfluencia{title}({fatherId}).txt";
			var path = _assemblyPath + @"\" + fileName;
			
			File.WriteAllLines(path, ids);
		}

		private void OutPutFnSummaryFile(string title, string id)
		{
			var fileName = $"CentralidadInfluencia{title}(resumen).txt";
			var path = _assemblyPath + @"\" + fileName;

			using (var file = new StreamWriter(path, true))
			{
				file.WriteLine("{0} {1}", id, NodeGraph[id].InfluenceSize);
			}
		}

		private void OutPutRetweetsFile(string id, double impact)
		{
			var fileName = "RetwittImpact(resumen).txt";
			var path = _assemblyPath + @"\" + fileName;

			using (var file = new StreamWriter(path, true))
			{
				file.WriteLine("{0} {1}", id, impact);
			}
		}

		#endregion
	}
}
