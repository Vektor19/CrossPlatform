namespace Lab3.Tests
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
        [InlineData(
        new string[]
        {
            "5 3 2 1",
            "2 3",
            "1 2 4",
            "2 3 5",
            "3 4 6"
        },
        5, 3, 2, 1,
        new int[] { 2, 3 },
        new int[] { 1, 2, 4, 2, 3, 5, 3, 4, 6 }
        )]
        [InlineData(
        new string[]
        {
            "4 2 1 2",
            "3",
            "1 3 10",
            "3 4 15"
        },
        4, 2, 1, 2,
        new int[] { 3 },
        new int[] { 1, 3, 10, 3, 4, 15 }
        )]
        public void ProcessInput_ValidData_ReturnsExpectedResult(string[] input, int expectedN, int expectedM, int expectedK, int expectedCapital, int[] expectedCities, int[] expectedRoads)
        {
            var result = Utils.ProcessInput(input);

            Assert.Equal(expectedN, result.Item1);
            Assert.Equal(expectedM, result.Item2);
            Assert.Equal(expectedK, result.Item3);
            Assert.Equal(expectedCapital, result.Item4);
            Assert.Equal(expectedCities, result.Item5);

            var flattenedRoads = new List<int>();
            foreach (var road in result.Item6)
            {
                flattenedRoads.Add(road.Item1);
                flattenedRoads.Add(road.Item2);
                flattenedRoads.Add(road.Item3);
            }
            Assert.Equal(expectedRoads, flattenedRoads);
        }

        [Theory]
        [MemberData(nameof(TestData.InvalidProcessInputData), MemberType = typeof(TestData))]
        public void ProcessInput_InvalidData_ThrowsFormatException(string[] input)
        {
            Assert.Throws<FormatException>(() => Utils.ProcessInput(input));
        }
    }
    public class TestData
    {
        public static IEnumerable<object[]> InvalidProcessInputData =>
            new List<object[]>
            {
            new object[] { new string[] { "Invalid data" } },
            new object[] { new string[] { "1 2 3", "Invalid cities" } },
            new object[] { new string[] { "1 2 3 4", "1 2", "Invalid road" } },
            new object[] { new string[] { "1 2 3 4", "1 2", "3 4 abcdsfsd" } },
            };
    }
}