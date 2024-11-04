using System.Text;

namespace Compressors
{
    public class LZWCompressor : ICompressor
    {
        private const int InitialDictionarySize = 256;

        public byte[] Compress(byte[] input)
        {
            // Initialize the dictionary with single-byte values
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < InitialDictionarySize; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            int dictSize = InitialDictionarySize;
            List<int> compressedData = new List<int>();

            // Iteratively compress the input
            StringBuilder w = new StringBuilder(); // Represents the current sequence
            foreach (byte b in input)
            {
                char c = (char)b;
                string wc = w.ToString() + c;

                if (dictionary.ContainsKey(wc))
                {
                    w.Append(c);
                }
                else
                {
                    compressedData.Add(dictionary[w.ToString()]); // Add code for 'w'
                                                                  // Add 'wc' to the dictionary if within limits
                    if (dictSize < 4096) // Adjust size limit as needed
                    {
                        dictionary[wc] = dictSize++;
                    }
                    w.Clear().Append(c); // Reset 'w' to the current character
                }
            }

            // Output the code for the last sequence 'w'
            if (w.Length > 0)
            {
                compressedData.Add(dictionary[w.ToString()]);
            }

            // Convert the list of integers to a byte array
            return IntListToByteArray(compressedData);
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // Initialize the dictionary with single-byte values
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < InitialDictionarySize; i++)
            {
                dictionary[i] = ((char)i).ToString();
            }

            int dictSize = InitialDictionarySize;
            List<byte> decompressedData = new List<byte>();

            // Convert the byte array to a list of codes
            List<int> codes = ByteArrayToIntList(compressedData);

            string w = dictionary[codes[0]]; // Initialize w as the first code's entry
            decompressedData.AddRange(Encoding.UTF8.GetBytes(w));

            for (int i = 1; i < codes.Count; i++)
            {
                int k = codes[i];
                string entry;

                if (dictionary.ContainsKey(k))
                {
                    entry = dictionary[k];
                }
                else if (k == dictSize)
                {
                    entry = w + w[0]; // Special case for KwKwK
                }
                else
                {
                    throw new ArgumentException("Invalid compressed data");
                }

                decompressedData.AddRange(Encoding.UTF8.GetBytes(entry));

                // Add new entry to the dictionary
                if (dictSize < 4096) // Adjust size limit as needed
                {
                    dictionary[dictSize++] = w + entry[0];
                }

                w = entry; // Set w to entry
            }

            return decompressedData.ToArray();
        }

        private byte[] IntListToByteArray(List<int> data)
        {
            List<byte> bytes = new List<byte>();
            foreach (int val in data)
            {
                bytes.Add((byte)(val >> 8 & 0xFF)); // Higher 8 bits
                bytes.Add((byte)(val & 0xFF));        // Lower 8 bits
            }
            return bytes.ToArray();
        }

        private List<int> ByteArrayToIntList(byte[] bytes)
        {
            List<int> data = new List<int>();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                int value = bytes[i] << 8 | bytes[i + 1]; // Combine two bytes to form an integer
                data.Add(value);
            }
            return data;
        }
    }
}