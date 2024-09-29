namespace Lab1.Tests
{
    public class TestUtils
    {
        [Theory]
        [InlineData("1")]
        [InlineData("1235465467547474")]
        [InlineData("900000112365412345678909876545123455677")]
        [InlineData("10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")] // 10^100
        public void isInputValid_ValidInput_ReturnsTrue(string input)
        {
            bool result = Utils.isInputValid(input);
            Assert.True(result);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("0")]
        [InlineData("100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")] // 10^101
        public void isInputValid_InvalidInput_ReturnsFalse(string input)
        {
            bool result = Utils.isInputValid(input);
            Assert.False(result);
        }

        [Fact]
        public void ReadInput_ValidPath_ReturnsCorrectLines()
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
        public void WriteResult_ValidPath_WritesCorrectLines()
        {
            string path = "testOutput.txt";
            string[] lines = { "result1", "result2", "result3" };

            Utils.WriteResult(path, lines);
            
            string[] writtenLines = File.ReadAllLines(path);
            Assert.Equal(lines, writtenLines);
        }
    }
}