using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3Library;

namespace Lab3.Tests
{
    public class TestTaskSolver
    {
        public class TaskSolverTests
        {
            [Theory]
            [InlineData(
                4, 4, 2, 1,
                new int[] { 3, 4 },
                new int[] { 1, 2, 5, 1, 3, 10, 2, 4, 3, 3, 4, 6 },
                new string[] { "4 8", "3 10" }
            )]
            [InlineData(
                3, 3, 1, 1,
                new int[] { 3 },
                new int[] { 1, 2, 4, 2, 3, 6, 1, 3, 9 },
                new string[] { "3 9" }
            )]
            [InlineData(
                5, 6, 3, 1,
                new int[] { 2, 4, 5 },
                new int[] { 1, 2, 1, 1, 3, 4, 2, 4, 2, 3, 4, 3, 3, 5, 2, 4, 5, 3 },
                new string[] { "2 1", "4 3", "5 6" }
            )]
            public void SolveTask_ValidData_ReturnsExpectedResult(int n, int m, int k, int capital, int[] citiesArray, int[] roadsArray, string[] expected)
            {
                var cities = new HashSet<int>(citiesArray);
                var roads = new List<(int, int, int)>();
                for (int i = 0; i < roadsArray.Length; i += 3)
                {
                    roads.Add((roadsArray[i], roadsArray[i + 1], roadsArray[i + 2]));
                }
                var result = TaskSolver.SolveTask(n, m, k, capital, cities, roads);

                Assert.Equal(expected, result);
            }
        }
    }
}
