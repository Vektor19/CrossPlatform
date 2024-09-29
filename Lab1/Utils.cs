using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
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
        public static bool isInputValid(string input)
        {
            if (!BigInteger.TryParse(input, out BigInteger number))
            {
                Console.WriteLine("Incorrect number: " + input);
                return false;
            }

            BigInteger lowerBound = BigInteger.One;
            BigInteger upperBound = BigInteger.Pow(10, 100);

            return number >= lowerBound && number <= upperBound;
        }
    }
}
