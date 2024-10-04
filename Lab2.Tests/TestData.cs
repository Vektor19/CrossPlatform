using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> IsArrayOfStringsAllIntegersData =>
            new List<object[]>
            {
            new object[] { new string[] { "1 2 3 4", "5 6 7 8" } },
            new object[] { new string[] { "11 3 4", "58" } },
            new object[] { new string[] { "19", "5 832 56 1 0 94 435 3" } }
            };
        public static IEnumerable<object[]> IsArrayOfStringsAllIntegersFalseData =>
            new List<object[]>
            {
                new object[] { new string[] { "1 2 3 a", "5 6 7 8" } },
                new object[] { new string[] { "11 3 4", "hello world" } },
                new object[] { new string[] { "123", "45.67" } }
            };
        public static IEnumerable<object[]> ProcessInputData =>
            new List<object[]>
            {
                new object[]
                {
                    new string[]
                    {
                        "3 4",
                        "1 1 1 1",
                        "5 2 2 100",
                        "9 4 2 1",
                        "5 5",
                        "1 1 1 1 1",
                        "3 100 100 100 100",
                        "1 1 1 1 1",
                        "2 2 2 2 1",
                        "1 1 1 1 1",
                        "2 3",
                        "2 a b",
                        "5 2 100",
                        "3 2",
                        "87 67",
                        "15 9 12 54",
                        "87 4",
                        "1 3",
                        "10 20 30"
                    },
                    new List<int[][]>
                    {
                        new int[][]
                        {
                            new int[] { 1, 1, 1, 1 },
                            new int[] { 5, 2, 2, 100 },
                            new int[] { 9, 4, 2, 1 }
                        },
                        new int[][]
                        {
                            new int[] { 1, 1, 1, 1, 1 },
                            new int[] { 3, 100, 100, 100, 100 },
                            new int[] { 1, 1, 1, 1, 1 },
                            new int[] { 2, 2, 2, 2, 1 },
                            new int[] { 1, 1, 1, 1, 1 }
                        },
                        null,
                        null,
                        new int[][]
                        {
                            new int[] { 10, 20, 30 }
                        }
                    }
                }
            };
        public static IEnumerable<object[]> SolveTaskData =>
                new List<object[]>
                {
                    new object[]
                    {
                        new int[][]
                        {
                            new int[] { 1, 1, 1, 1 },
                            new int[] { 5, 2, 2, 100 },
                            new int[] { 9, 4, 2, 1 }
                        },
                        8
                    },
                    new object[]
                    {
                        new int[][]
                        {
                            new int[] { 1, 1, 1, 1, 1 },
                            new int[] { 3, 100, 100, 100, 100 },
                            new int[] { 1, 1, 1, 1, 1 },
                            new int[] { 2, 2, 2, 2, 1 },
                            new int[] { 1, 1, 1, 1, 1 }
                        },
                        11
                    },
                    new object[]
                    {
                        new int[][]
                        {
                            new int[] { 1, 3, 1 },
                            new int[] { 1, 5, 1 },
                            new int[] { 4, 2, 1 }
                        },
                        7
                    },
                    new object[]
                    {
                        new int[][]
                        {
                            new int[] { 1, 2, 19 },
                            new int[] { 1, 18, 2 }
                        },
                        22
                    }
                };
    }
}
