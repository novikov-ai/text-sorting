using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SortingUtil
{
    public class TextSorter
    {
        private readonly string _inputPath;

        private const string Postfix = "_sorted.txt";

        private const int BufferSize = 64 * 1024;

        private static readonly char DirSeparator = Path.DirectorySeparatorChar;

        private static readonly string DefaultDir =
            $@"..{DirSeparator}..{DirSeparator}..{DirSeparator}..{DirSeparator}TxtFiles{DirSeparator}";

        public TextSorter(string path = null)
        {
            _inputPath = !string.IsNullOrEmpty(path) ? path : Directory.GetFiles(DefaultDir).FirstOrDefault();

            if (!File.Exists(_inputPath))
                throw new ArgumentException("File does not exist.");
        }

        public void Sort()
        {
            var inputExtensionIndex = _inputPath.LastIndexOf('.');
            var outputFilePath = _inputPath.Substring(0, inputExtensionIndex) + Postfix;

            int inputFileSize = (int) new FileInfo(_inputPath).Length;

            var loaded = new List<Record>(inputFileSize / 25); // 25 - average one line length

            using (var sr = new StreamReader(_inputPath, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    loaded.Add(new Record(line));
                }
            }

            loaded.Sort();

            using (var sw = new StreamWriter(outputFilePath, false, Encoding.UTF8, BufferSize))
            {
                foreach (var kvp in loaded)
                {
                    sw.WriteLine(kvp);
                }
            }
        }
    }
}