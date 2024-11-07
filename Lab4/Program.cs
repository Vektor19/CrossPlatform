using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Lab4
{
    [Subcommand(typeof(VersionCommand), typeof(Run), typeof(SetPathCommand))]
    [HelpOption("-h|--help")]
    public class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine("Please specify a command (version, run, set-path).");
        }
    }

    [Command(Name = "version", Description = "Version of program")]
    class VersionCommand
    {
        private void OnExecute(IConsole console)
        {
            console.WriteLine("Author: Komissarov V.R.");
            console.WriteLine("Version: 1.0");
        }
    }

    [Command(Name = "run", Description = "Run the program")]
    class Run
    {
        [Required]
        [Argument(0)]
        public string? LabName { get; }

        [Option("-i|-I|--input", Description = "Input file path")]
        public string? InputPath { get; }

        [Option("-o|-O|--output", Description = "Output file path")]
        public string? OutputPath { get; }

        private readonly string[] _labNames = { "Lab1", "Lab2", "Lab3" };

        private void OnExecute()
        {
            if (string.IsNullOrEmpty(LabName))
            {
                Console.WriteLine("Error: Lab name is required.");
                return;
            }
            string processedLabName = ProcessLabStringName(LabName);

            if(!_labNames.Contains(processedLabName))
            {
                Console.WriteLine("Error: Invalid lab name.");
                Console.WriteLine($"Available labs: {string.Join(", ", _labNames)}");
                return;
            }

            Console.WriteLine($"Running {processedLabName}...");

            string inputFilePath = ResolveFilePath(InputPath, "input.txt");
            if (string.IsNullOrEmpty(inputFilePath))
            {
                Console.WriteLine("Error: Input file could not be found.");
                return;
            }

            string outputFilePath = ResolveFilePath(OutputPath, "output.txt");
            if (string.IsNullOrEmpty(outputFilePath))
            {
                Console.WriteLine("Error: Output file could not be found. Creating output.txt in the same directory as input.txt ...");
                string? directory = Path.GetDirectoryName(inputFilePath);
                if (string.IsNullOrEmpty(directory))
                {
                    Console.WriteLine("Error: Could not determine directory of input file.");
                    return;
                }
                outputFilePath = Path.Combine(directory, "output.txt");
            }

            try
            {
                Console.WriteLine($"Path to input file: {inputFilePath}\nPath to output file: {outputFilePath}");
                LabsLibrary.LabsRunner.ExecuteLab(processedLabName, inputFilePath, outputFilePath);
                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during execution: " + ex.Message);
            }
        }

        private string ResolveFilePath(string? consolePath, string fileName)
        {
            if (!string.IsNullOrEmpty(consolePath) && File.Exists(consolePath))
            {
                return consolePath;
            }
            EnvironmentVariableTarget target = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Process;
            string? envPath = Environment.GetEnvironmentVariable("LAB_PATH", target);

            if (!string.IsNullOrEmpty(envPath))
            {
                var envFilePath = FindFileIgnoreCase(envPath, fileName);
                if (!string.IsNullOrEmpty(envFilePath))
                {
                    return envFilePath;
                }
            }

            string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var homeFilePath = FindFileIgnoreCase(homePath, fileName);
            if (!string.IsNullOrEmpty(homeFilePath))
            {
                return homeFilePath;
            }

            return String.Empty;
        }

        private string FindFileIgnoreCase(string directory, string fileName)
        {
            foreach (var file in Directory.EnumerateFiles(directory, fileName, SearchOption.TopDirectoryOnly))
            {
                if (string.Equals(Path.GetFileName(file), fileName, StringComparison.OrdinalIgnoreCase))
                {
                    return file;
                }
            }
            return String.Empty;
        }
        private static string ProcessLabStringName(string labName)
        {
            labName = labName.ToLower();
            return char.ToUpper(labName[0]) + labName.Substring(1);
        }
    }

    [Command(Name = "set-path", Description = "Set the path for input and output files directory")]
    class SetPathCommand
    {
        [Required]
        [Option("-p|--path", Description = "Path to the folder with input and output files", ShowInHelpText = true)]
        public string? PathToFolder { get; }

        private void OnExecute()
        {
            try
            {
                EnvironmentVariableTarget target = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Process;
                Environment.SetEnvironmentVariable("LAB_PATH", PathToFolder, target);
                Console.WriteLine($"LAB_PATH set to {Environment.GetEnvironmentVariable("LAB_PATH")}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting LAB_PATH: " + ex.Message);
            }
        }
    }
}
