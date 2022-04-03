using System.Linq;

namespace SortingUtil
{
    public static class BytesExtension
    {
        public static int Compare(this byte[] first, byte[] second)
        {
            if (first.SequenceEqual(second))
                return 0;

            var minArray = first.Length > second.Length ? second : first;

            for (int i = 0; i < minArray.Length; i++)
            {
                if (first[i] == second[i])
                    continue;

                return first[i] > second[i] ? 1 : -1;
            }

            return minArray == second ? 1 : -1;
        }
    }
}