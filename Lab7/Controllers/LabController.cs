using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
    [Authorize]
    public class LabController : Controller
    {

        public IActionResult Lab1()
        {
            return View();
        }
        public IActionResult Lab2()
        {
            return View();
        }
        public IActionResult Lab3()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Execute(string labName, string inputData)
        {
            string tempDirectory = Path.GetTempPath();
            string inputFilePath = Path.Combine(tempDirectory, "input.txt");
            string outputFilePath = Path.Combine(tempDirectory, "output.txt");
                
            if (!System.IO.File.Exists(inputFilePath))
            {
                System.IO.File.Create(inputFilePath).Dispose();
            }
            System.IO.File.WriteAllText(inputFilePath, inputData);


            LabsLibrary.LabsRunner.ExecuteLab(labName, inputFilePath, outputFilePath);
            string outputContent = System.IO.File.ReadAllText(outputFilePath);
            System.IO.File.Delete(inputFilePath);
            System.IO.File.Delete(outputFilePath);
            ViewData["Result"] = outputContent;
            ViewData["InputData"] = inputData;

            return View(labName);
        }
    }
}
