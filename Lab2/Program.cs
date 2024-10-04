using Lab2;

string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\INPUT.txt");
string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\OUTPUT.txt");

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