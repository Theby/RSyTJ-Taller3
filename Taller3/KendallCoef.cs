using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Taller3
{
	public static class KendallCoef
	{
		public static double KendallCoefCalculation(string mainFileName, string secondFileName)
		{
			double kendall = 0;
			var mainList = ReadFile(mainFileName);

			if (mainList != null)
			{
				mainList.Sort(((d, d1) => d.Item2.CompareTo(d1.Item2)));
				var mainListIDs = mainList.Select(tuple => tuple.Item1).ToList();
				Console.WriteLine(">> Main File Sorted");
				
				var secondList = ReadFile(secondFileName);

				if (secondList != null)
				{
					secondList = secondList.OrderBy(d => mainListIDs.IndexOf(d.Item1)).ToList();
					Console.WriteLine(">> Second File Sorted");
					var secondListValues = secondList.Select(tuple => tuple.Item2).ToList();

					Console.WriteLine("> Calculating Kendall: ");
					var factorResult = FactorCalculation(secondListValues);
					var rankNumbers = secondListValues.Count;
					kendall = factorResult / (0.5f * rankNumbers * (rankNumbers - 1));

					Console.WriteLine(">> {0} vs {1} = {2} ", mainFileName, secondFileName, kendall);								
				}
			}

			return kendall;
		}

		private static double FactorCalculation(List<double> pageRanks)
		{
			var downValues = new List<double>(pageRanks);

			double concardant = 0.0f;
			double discordant = 0.0f;

			foreach (var pageRank in pageRanks)
			{
				downValues.RemoveAt(0);

				if (downValues.Count == 0)
					break;

				concardant += downValues.Count(pr => pageRank < pr);
				discordant += downValues.Count(pr => pageRank > pr);
			}

			return concardant - discordant;
		}


		private static List<Tuple<double, double>> ReadFile(string fileName)
		{
			var pageRanks = new List<Tuple<double, double>>();

			try
			{   // Open the text file using a stream reader.
				using (var sr = new StreamReader(fileName))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						var lineWord = line.Split(',');
						var id = Convert.ToDouble(lineWord[0]);
						var size = Convert.ToDouble(lineWord[1]);

						var tuple = new Tuple<double, double>(id, size);

						pageRanks.Add(tuple);
					}

					sr.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}

			return pageRanks;
		}
	}
}
