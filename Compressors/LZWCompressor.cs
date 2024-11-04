using System.Text;

namespace Compressors
{
    public class LZWCompressor
    {
        public static List<int> Compress(string input)
        {
            // Initialize the dictionary with single-character strings
            Dictionary<string, int> dictionary = [];
            for (int i = 0; i < char.MaxValue; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            string current = string.Empty;
            List<int> compressed = [];

            foreach (char symbol in input)
            {
                string combined = current + symbol;
                if (dictionary.ContainsKey(combined))
                {
                    current = combined;
                }
                else
                {
                    compressed.Add(dictionary[current]);
                    dictionary.Add(combined, dictionary.Count);
                    current = symbol.ToString();
                }
            }

            // Add the last string to the compressed list
            if (!string.IsNullOrEmpty(current))
            {
                compressed.Add(dictionary[current]);
            }

            return compressed;
        }

        public static string Decompress(List<int> compressed)
        {
            // Initialize the dictionary with single-character strings
            Dictionary<int, string> dictionary = [];
            for (int i = 0; i < char.MaxValue; i++)
            {
                dictionary.Add(i, ((char)i).ToString());
            }

            string current = dictionary[compressed[0]];
            StringBuilder decompressed = new(current);

            for (int i = 1; i < compressed.Count; i++)
            {
                string entry;
                int code = compressed[i];

                if (dictionary.TryGetValue(code, out string? value))
                {
                    entry = value;
                }
                else if (code == dictionary.Count)
                {
                    entry = current + current[0];
                }
                else
                {
                    throw new Exception("Invalid compressed code.");
                }

                decompressed.Append(entry);

                // Add new entry to the dictionary
                dictionary.Add(dictionary.Count, current + entry[0]);

                current = entry;
            }

            return decompressed.ToString();
        }
    }
}