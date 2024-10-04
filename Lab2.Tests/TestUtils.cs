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

}