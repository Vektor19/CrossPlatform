namespace Lab2
{
    public class TaskSolver
    {
        public static int SolveTask(int[][] inputArray)
        {
            int n = inputArray.Length;
            int m = inputArray[0].Length;

            int[,] pathCost = new int[n, m];

            pathCost[0, 0] = inputArray[0][0];

            for (int i = 1; i < n; ++i)
            {
                pathCost[i, 0] = inputArray[i][0] + pathCost[i - 1, 0];
            }
            for (int i = 1; i < m; ++i)
            {
                pathCost[0, i] = inputArray[0][i] + pathCost[0, i - 1];
            }
            for (int i = 1; i < n; ++i)
            {
                for (int j = 1; j < m; ++j)
                {
                    pathCost[i, j] = Math.Min(pathCost[i - 1, j], pathCost[i, j - 1]) + inputArray[i][j];
                }
            }
            return pathCost[n - 1, m - 1];
        }
    }
}
