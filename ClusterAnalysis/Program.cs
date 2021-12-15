using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace ClusterAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyName projAssemblyName = GetAssemblyName();

            SetupLogger(projAssemblyName.Name);

            Log.Information($"{projAssemblyName.Name} {projAssemblyName.Version} started at {DateTime.Now}");

            Console.WriteLine("Press ESC to stop.");

            //Timer timer = new(Analyze, null, 0, 10000); // set timer to re-run in loop
            Analyze(null);

            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;

            Log.Information($"{projAssemblyName.Name} ended at {DateTime.Now}");
            Log.CloseAndFlush();
        }

        private static void Analyze(Object o)
        {
            Batch _batch = new();

            _batch.Run();

            Console.Write("."); // when timer is running, this prints a dot to the console just so you know something is still happening
        }

        private static void SetupLogger(string projName)
        {
            // check folder exists
            var folderPath = @"C:\Logs";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // set up logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(folderPath + @"\" + projName + ".txt", rollingInterval: RollingInterval.Month)
                .CreateLogger();
        }

        private static AssemblyName GetAssemblyName()
        {
            Assembly assembly = typeof(Program).Assembly;
            
            var assemblyName = assembly.GetName();

            return assemblyName;
        }
    }
}