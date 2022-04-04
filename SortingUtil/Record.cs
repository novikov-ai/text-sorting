using System;

namespace SortingUtil
{
    public struct Record : IComparable
    {
        public string Sentence;
        public ushort Number;

        internal Record(string record)
        {
            var splited = record.Split(". ", 2);
            Number = ushort.Parse(splited[0]);
            Sentence = splited[1];
        }

        public override string ToString()
        {
            return $"{Number}. {Sentence}";
        }

        public int CompareTo(object obj)
        {
            var record = (Record) obj;

            int result = string.Compare(this.Sentence, record.Sentence, StringComparison.Ordinal);
            return result == 0 ? this.Number.CompareTo(record.Number) : result;
        }
    }
}