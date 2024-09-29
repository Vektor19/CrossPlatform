using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class TaskSolver
    {
        public static int SolveTask(string inputValue)
        {
            int[] numbers = new int[1000];
            int totalCount = 0;

            int digit1, digit2, digit3;
            if (inputValue.Length >= 3)
            {
                for (int i = 0; i < inputValue.Length - 2; i++)
                {
                    digit1 = int.Parse(inputValue[i].ToString());
                    for (int j = i + 1; j < inputValue.Length - 1; j++)
                    {
                        digit2 = int.Parse(inputValue[j].ToString());
                        for (int k = j + 1; k < inputValue.Length; k++)
                        {
                            digit3 = int.Parse(inputValue[k].ToString());
                            int number = digit1 * 100 + digit2 * 10 + digit3;
                            if (numbers[number] == 0 && number > 99)
                            {
                                numbers[number]++;
                                totalCount++;
                            }
                        }
                    }
                }
            }
            return totalCount;
        }
    }
}
