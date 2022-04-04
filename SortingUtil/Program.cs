using System;
using System.Diagnostics;
using System.IO;

namespace SortingUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine(@"Provide path to txt-file or push enter for getting a first occurrence in default directory matching with <*txt>.
Sorted file will be created nearby.");

            var filePath = Console.ReadLine();

            try
            {
                new TextSorter(filePath).Sort();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error happened: {e.Message}");
                throw;
            }

            stopwatch.Stop();
            Console.WriteLine($@"File was successfully sorted!
Time consumed: {stopwatch.Elapsed}");
        }
    }
}