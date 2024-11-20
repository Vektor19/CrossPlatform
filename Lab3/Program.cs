using Lab3;
using Lab3Library;
string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\INPUT.txt");
string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\OUTPUT.txt");

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
