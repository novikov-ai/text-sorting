using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneratorUtil
{
    public class SentenceGenerator
    {
        private const string RecordSeparator = ". ";

        // private readonly string _path = "../../../../TxtFiles/generated.txt";
        private readonly string _path = @"\..\..\..\..\TxtFiles\generated.txt";

        private static Random _random;

        // private const string DictionaryPathNouns = "../../../Dictionaries/nouns.txt";
        private const string DictionaryPathNouns = @"..\..\..\Dictionaries\nouns.txt";
        // private const string DictionaryPathAdverbs = "../../../Dictionaries/adverbs.txt";
        private const string DictionaryPathAdverbs = @"..\..\..\Dictionaries\adverbs.txt";
        // private const string DictionaryPathAdjectives = "../../../Dictionaries/adjectives.txt";
        private const string DictionaryPathAdjectives = @"..\..\..\Dictionaries\adjectives.txt";

        private readonly string[] _nouns;
        private readonly string[] _adverbs;
        private readonly string[] _adjectives;

        public SentenceGenerator(string path = null)
        {
            if (!string.IsNullOrEmpty(path))
                _path = path;

            _random = new Random();

            _nouns = File.ReadAllLines(DictionaryPathNouns);
            _adverbs = File.ReadAllLines(DictionaryPathAdverbs);
            _adjectives = File.ReadAllLines(DictionaryPathAdjectives);

            if (!_nouns.Any() || !_adverbs.Any() || !_adjectives.Any())
            {
                throw new ArgumentException($@"Dictionaries wasn't found. 

Check if dictionaries exists:
- Nouns path: {DictionaryPathNouns}
- Adverbs path: {DictionaryPathAdverbs}
- Adjectives path: {DictionaryPathAdjectives}");
            }
        }

        public void Generate(long mb)
        {
            var byteSum = 1024 * 1024 * mb;

            if (byteSum < 0)
                throw new ArgumentException("Provide less size.");

            var random = new Random();

            const int BufferSize = 65536;

            using (var sw = new StreamWriter(_path, true, Encoding.UTF8, BufferSize))
            {
                string cachedSentence = null;

                while (byteSum > 0)
                {
                    var generatedSentence = GenerateSentence();

                    if (cachedSentence != null && byteSum % 9 == 0)
                    {
                        generatedSentence = cachedSentence;
                    }

                    var generatedLine = string.Join(RecordSeparator, new string[2]
                    {
                        random.Next(1, 1000).ToString(),
                        generatedSentence
                    });

                    sw.WriteLine(generatedLine);

                    byteSum = byteSum - generatedLine.Length - 1;

                    cachedSentence = generatedSentence;
                }
            }
        }

        private string GenerateSentence()
        {
            return $"{GetRandomWord(_nouns)} is {GetRandomWord(_adverbs)} {GetRandomWord(_adjectives)}";
        }

        private static string GetRandomWord(string[] dictionary)
        {
            return dictionary[_random.Next(1, dictionary.Length - 1)];
        }
    }
}