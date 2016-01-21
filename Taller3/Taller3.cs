using System;

namespace Taller3
{
	internal class Taller3
	{
		private static void Main()
		{
			Console.WriteLine("> 'Node ID and Tag' File Source Name: ");
			var fileName = Console.ReadLine();

			var graphNode = new Graph();

			if (!graphNode.PopulateFromFile(fileName))
			{
				Console.WriteLine(">> Error reading file: " + fileName);
				Console.ReadLine();
				return;
			}

			Console.WriteLine("> File read succefull");
			Console.WriteLine("> 'Node ID, weight and Target Node' File Source Name: ");
			fileName = Console.ReadLine();

			if (!graphNode.ConectNodesFromFile(fileName))
			{
				Console.WriteLine(">> Error reading file: " + fileName);
				Console.ReadLine();
				return;
			}

			Console.WriteLine("> File read succefull");

			foreach (var node in graphNode.NodeGraph)
			{
				graphNode.F3(node.Key);
				graphNode.RetweetImpact(node.Key);
			}

			KendallCoef.KendallCoefCalculation(graphNode.FileNames[0], graphNode.FileNames[1]);
			KendallCoef.KendallCoefCalculation(graphNode.FileNames[0], graphNode.FileNames[2]);
			KendallCoef.KendallCoefCalculation(graphNode.FileNames[1], graphNode.FileNames[2]);

			Console.WriteLine("> Execution Done");
			Console.ReadLine();
		}
	}
}
