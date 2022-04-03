using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SortingUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Provide path to txt-file. Sorted file will be created nearby.");

            var filePath = Console.ReadLine();

            if (!File.Exists(filePath))
                throw new ArgumentException("File does not exist.");

            var inputExtensionIndex = filePath.LastIndexOf('.');
            var outputFilePath = filePath.Substring(0, inputExtensionIndex) + "_sorted.txt";

            const int BufferSize = 65536;
            var loaded = new List<Record>(BufferSize);

            using (var sr = new StreamReader(filePath, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var splited = line.Split(". ", 2);
                    var record = new Record()
                    {
                        Number = ushort.Parse(splited[0]),
                        Sentence = splited[1]
                    };

                    loaded.Add(record);

                    // loaded.Add(new Record(line));
                }
            }

            Console.WriteLine($"Txt loaded into memory: {stopwatch.Elapsed}");
            // => 00:36

            loaded.Sort();
            Console.WriteLine($"Sorted: {stopwatch.Elapsed}");

            // => 2:17
            using (var sw = new StreamWriter(outputFilePath, false, Encoding.UTF8, BufferSize))
            {
                foreach (var kvp in loaded)
                {
                    sw.WriteLine(kvp);
                }
            }

            // => 2:28

            stopwatch.Stop();
            Console.WriteLine($@"Success!
time: {stopwatch.Elapsed}");
        }
    }
}