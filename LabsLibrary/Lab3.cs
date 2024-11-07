using Lab3;

namespace LabsLibrary
{
    internal class Lab3: ILab
    {
        public void Run(string inputPath, string outputPath)
        {
            try
            {
                var (n, m, k, capital, cities, roads) = Utils.ProcessInput(Utils.ReadInput(inputPath));
                var solutions = TaskSolver.SolveTask(n, m, k, capital, cities, roads);
                Utils.WriteResult(outputPath, solutions);

            }
            catch (Exception)
            {
                Utils.WriteResult(outputPath, new string[] { "Bad Input" });
            }

        }
    }
}
