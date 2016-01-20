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

			graphNode.F3("0");

			Console.ReadLine();
		}
	}
}
