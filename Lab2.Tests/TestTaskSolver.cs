namespace Lab2.Tests
{
    public class TestTaskSolver
    {
        [Theory]
        [MemberData(nameof(TestData.SolveTaskData), MemberType = typeof(TestData))]
        public void SolveTask_ValidInput_ReturnsCorrectCount(int[][] inputArray, int expectedCount)
        {
            int result = TaskSolver.SolveTask(inputArray);
            Assert.Equal(expectedCount, result);
        }
    }
}