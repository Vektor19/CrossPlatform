using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Utils
    {
        public static string[] ReadInput(string path)
        {
            return File.ReadLines(path).ToArray();
        }

        public static void WriteResult(string path, string[] result)
        {
            File.WriteAllLines(path, result);
        }
        public static (int, int, int, int, HashSet<int>, List<(int, int, int)>) ProcessInput(string[] input)
        {
            if (isArrayOfStringsAllIntegers(input))
            {
                var firstLine = input[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = int.Parse(firstLine[0]);
                int m = int.Parse(firstLine[1]);
                int k = int.Parse(firstLine[2]);
                int capital = int.Parse(firstLine[3]);

                var cities = input[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();

                List<(int, int, int)> roads = new List<(int, int, int)>();
                for (int i = 2; i < 2 + m; i++)
                {
                    var roadData = input[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int s = int.Parse(roadData[0]);
                    int e = int.Parse(roadData[1]);
                    int t = int.Parse(roadData[2]);
                    roads.Add((s, e, t));
                }

                return (n, m, k, capital, cities, roads);
            }
            throw new FormatException();
        }

        public static bool isArrayOfStringsAllIntegers(string[] input)
        {
            foreach (var str in input)
            {
                string[] parts = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                {
                    if (!int.TryParse(part, out _))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
