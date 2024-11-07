using Lab1;

namespace LabsLibrary
{
    internal class Lab1: ILab
    {
        public void Run(string inputPath, string outputPath)
        {
            string[] inputValues = Utils.ReadInput(inputPath);
            string[] solutions = new string[inputValues.Length];

            for (int i = 0; i < inputValues.Length; i++)
            {
                if (!Utils.isInputValid(inputValues[i])) solutions[i] = "Bad input";
                else solutions[i] = TaskSolver.SolveTask(inputValues[i]).ToString();
            }
            Utils.WriteResult(outputPath, solutions);
        }
    }
}
