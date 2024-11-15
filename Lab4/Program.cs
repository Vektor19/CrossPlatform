using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
            //EnvironmentVariableTarget target = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Process;
            string? envPath = EnvironmentManager.Get("LAB_PATH");
            Console.WriteLine($"LAB_PATH: {envPath}");

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
            if (!Directory.Exists(directory))
            {
                return String.Empty;
            }

            foreach (var file in Directory.EnumerateFiles(directory))
            {
                if (string.Equals(Path.GetFileName(file), fileName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"found file:" + file);
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
                EnvironmentManager.Set("LAB_PATH", PathToFolder!);
                Console.WriteLine($"LAB_PATH set to {PathToFolder}");
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting LAB_PATH: " + ex.Message);
            }
        }
    }
}

public class EnvironmentManager
{
    public static string? Get(string variableName)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return System.Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);

        string? result = null;
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"echo ${variableName}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        proc.Start();
        while (!proc.StandardOutput.EndOfStream)
            result = proc.StandardOutput.ReadLine();

        proc.WaitForExit();
        return result;
    }

    public static void Set(string variableName, string value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            System.Environment.SetEnvironmentVariable(variableName, value, EnvironmentVariableTarget.User);
            return;
        }

        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"echo export {variableName}={value}>>~/.bashrc; source ~/.bashrc\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        proc.Start();
        proc.WaitForExit();
    }
}