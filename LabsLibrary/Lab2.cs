using Lab2;

namespace LabsLibrary
{
    internal class Lab2: ILab
    {
        public void Run(string inputPath, string outputPath)
        {
            List<int[][]> inputArrays = Utils.ProcessInput(Utils.ReadInput(inputPath));
            string[] solutions = new string[inputArrays.Count];

            for (int i = 0; i < inputArrays.Count; i++)
            {
                var arr = inputArrays[i];
                if (arr is null)
                {
                    solutions[i] = "Bad input";
                }
                else
                {
                    solutions[i] = TaskSolver.SolveTask(arr).ToString();
                }
            }

            Utils.WriteResult(outputPath, solutions);
        }
    }
}
