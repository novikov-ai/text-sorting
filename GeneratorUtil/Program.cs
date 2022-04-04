using System;
using System.Diagnostics;
using System.IO;

namespace GeneratorUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            Console.WriteLine(@"Default generating file is 1000MB (1GB).
If you want change it size provide value in MB or enter any key.");
            if (!int.TryParse(Console.ReadLine(), out var fileSizeMb))
            {
                fileSizeMb = 1000;
            }

            // var filePath = $"../../../../TxtFiles/generated_mb{fileSizeMb}.txt";
            var filePath = $@"..\..\..\..\TxtFiles\generated_mb{fileSizeMb}.txt";
            Console.WriteLine($@"Default file path is: {filePath}");

            try
            {
                var sentenceGeneratorUtil = new SentenceGenerator(filePath);
            
                sentenceGeneratorUtil.Generate(fileSizeMb);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error happened: {e.Message}");
                throw;
            }
            
            stopwatch.Stop();
            
            Console.WriteLine($@"Text file was generated.
Time consumed: {stopwatch.Elapsed}");
            
            Console.ReadKey();
        }
    }
}