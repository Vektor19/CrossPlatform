using Lab1;
using Xunit;

namespace Lab1.Tests
{
    public class TestTaskSolver
    {
        [Theory]
        [InlineData("10099", 3)]
        [InlineData("11111111111100111111111", 4)]
        public void SolveTask_ValidInput_ReturnsCorrectCount(string input, int expectedCount)
        {
            int result = TaskSolver.SolveTask(input);
            Assert.Equal(result, expectedCount);
        }
        [Fact]
        public void SolveTask_InputHasLessThanThreeDigits_ReturnsZero()
        {
            string inputValue = "12";
            int result = TaskSolver.SolveTask(inputValue);
            Assert.Equal(0, result);
        }
        [Fact]
        public void SolveTask_InputHasNonNumericCharacters_ThrowsFormatException()
        {
            string inputValue = "abcde";
            Assert.Throws<FormatException>(() => TaskSolver.SolveTask(inputValue));
        }
    }
}
