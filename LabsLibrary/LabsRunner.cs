namespace LabsLibrary
{
    public class LabsRunner
    {
        public static void ExecuteLab(string labName, string inputPath, string outputPath)
        {
            Type? lab = Type.GetType($"LabsLibrary.{labName}");
            
            if (lab == null)
            {
                throw new Exception("Invalid lab name");
            }
            
            lab.GetMethod("Run")?.Invoke(Activator.CreateInstance(lab), new object[] { inputPath, outputPath });
        }

    }
}