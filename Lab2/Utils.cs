using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
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

        public static List<int[][]> ProcessInput(string[] input)
        {
            List<int[][]> arrays = new List<int[][]>();

            for (int i = 0; i < input.Length;)
            {
                int[][] array=null;
                string[] size = input[i].Split(' ');
                if (!int.TryParse(size[0], out int rows) || !int.TryParse(size[1], out int cols) || size.Length!=2)
                {
                    Console.WriteLine("Invalid format of values: " + size);
                    arrays.Add(array);
                    continue;
                }
                if (isArrayOfStringsAllIntegers(input[(i + 1)..(i + 1 + rows)]))
                {
                    array = new int[rows][];
                    for (int j = 0; j < rows; j++)
                    {
                        array[j] = new int[cols];
                    }
                    for (int j = 0; j < rows; j++)
                    {
                        string[] values = input[i + 1 + j].Split(' ');
                        if (values.Length != cols)
                        {
                            array = null;
                            break;
                        }
                        for (int k = 0; k < cols; k++)
                        {
                            array[j][k] = int.Parse(values[k]);
                        }
                    }
                }
                arrays.Add(array);
                i += rows + 1;
            }
            return arrays;
        }
        public static bool isArrayOfStringsAllIntegers(string[] input)
        {
            foreach (var str in input)
            {
                string[] parts = str.Split(' ');

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
