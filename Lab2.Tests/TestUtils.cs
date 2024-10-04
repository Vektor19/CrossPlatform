namespace Lab2.Tests
{
    public class TestUtils
    {
        [Fact]
        public void ReadInput_ReturnsCorrectLines()
        {
            string path = "testInput.txt";
            File.WriteAllLines(path, new[] { "line1", "line2", "line3" });
            string[] result = Utils.ReadInput(path);

            Assert.Equal(3, result.Length);
            Assert.Equal("line1", result[0]);
            Assert.Equal("line2", result[1]);
            Assert.Equal("line3", result[2]);
        }
        [Fact]
        public void WriteResult__WritesCorrectLines()
        {
            string path = "testOutput.txt";
            string[] lines = { "result1", "result2", "result3" };

            Utils.WriteResult(path, lines);

            string[] writtenLines = File.ReadAllLines(path);
            Assert.Equal(lines, writtenLines);
        }
        [Theory]
        [MemberData(nameof(TestData.IsArrayOfStringsAllIntegersData), MemberType = typeof(TestData))]
        public void isArrayOfStringsAllIntegers_ReturnsTrue(string[] input)
        {
            bool result = Utils.isArrayOfStringsAllIntegers(input);

            Assert.True(result);
        }
        [Theory]
        [MemberData(nameof(TestData.IsArrayOfStringsAllIntegersFalseData), MemberType = typeof(TestData))]
        public void isArrayOfStringsAllIntegers_ReturnsFalse(string[] input)
        {
            bool result = Utils.isArrayOfStringsAllIntegers(input);

            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(TestData.ProcessInputData), MemberType = typeof(TestData))]
        public void ProcessInput_ReturnsExpectedResult(string[] input, List<int[][]> expected)
        {
            List<int[][]> result = Utils.ProcessInput(input);

            Assert.Equal(expected.Count, result.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                if (expected[i] is null)
                {
                    Assert.Null(result[i]);
                }
                else
                {
                    Assert.Equal(expected[i].Length, result[i].Length);
                    for (int j = 0; j < expected[i].Length; j++)
                    {
                        Assert.Equal(expected[i][j], result[i][j]);
                    }
                }
            }
        }
    }
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
    }
}