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

			Console.WriteLine("==== Calculatin Fn(i) =====");
			foreach (var node in graphNode.NodeGraph)
			{
				graphNode.F3(node.Key);
				graphNode.RetweetImpact(node.Key);
			}
			Console.WriteLine("==== Fn(i) Done =====");

			Console.WriteLine("==== Calculatin Kandall =====");
			var file1 = graphNode.FileNames[0];
			var file2 = graphNode.FileNames[1];
			var file3 = graphNode.FileNames[2];

			var k1 = KendallCoef.KendallCoefCalculation(file1, file2);
			var k2= KendallCoef.KendallCoefCalculation(file1, file3);
			var k3= KendallCoef.KendallCoefCalculation(file2, file3);

			graphNode.OutPutKendallFile(file1, file2, k1);
			graphNode.OutPutKendallFile(file1, file3, k2);
			graphNode.OutPutKendallFile(file2, file3, k3);

			Console.WriteLine("==== Kandall Done =====");

			Console.WriteLine("> Execution Done");
			Console.ReadLine();
		}
	}
}
