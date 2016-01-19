using System;

namespace Taller3
{
	class Taller3
	{
		static void Main()
		{
			Console.WriteLine("> 'Node ID and Tag' File Source Name: ");
			var fileName = Console.ReadLine();

			var graphNode = new Graph();

			if (!graphNode.PopulateFromFile(fileName))
			{
				Console.WriteLine(">> Error reading file: " + fileName);
				return;
			}

			Console.WriteLine("> File read succefull");
			Console.WriteLine("> 'Node ID, weight and Target Node' File Source Name: ");
			fileName = Console.ReadLine();

			if (!graphNode.ConectNodesFromFile(fileName))
			{
				Console.WriteLine(">> Error reading file: " + fileName);
				return;
			}

			Console.WriteLine("> File read succefull");

			//if (testListOne != null)
			//{
			//	testListOne.Sort(((d, d1) => d.Item2.CompareTo(d1.Item2)));
			//	var testListOneIDs = testListOne.Select(tuple => tuple.Item1).ToList();
			//	Console.WriteLine(">> Main File Sorted");

			//	Console.WriteLine("> Second File Name: ");
			//	fileName = Console.ReadLine();
			//	var testListTwo = ReadFile(fileName);

			//	if (testListTwo != null)
			//	{
			//		testListTwo = testListTwo.OrderBy(d => testListOneIDs.IndexOf(d.Item1)).ToList();
			//		Console.WriteLine(">> Second File Sorted");
			//		var testListTwoValues = testListTwo.Select(tuple => tuple.Item2).ToList();

			//		Console.WriteLine("> Calculating Kendall: ");
			//		var factorResult = FactorCalculation(testListTwoValues);
			//		var rankNumbers = testListTwoValues.Count;
			//		var kendall = factorResult / (0.5f * rankNumbers * (rankNumbers - 1));

			//		Console.WriteLine(">> " + kendall);
			//		Console.WriteLine("> Done");
			//	}
			//}
			Console.ReadLine();
		}
	}
}
