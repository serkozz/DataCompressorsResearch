using System.Text;

namespace Compressors
{
    public class HuffmanCompressor : ICompressor
    {
        private class Node
        {
            public byte Symbol { get; set; }
            public int Frequency { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public bool IsLeaf => Left == null && Right == null;
        }

        private readonly Dictionary<byte, string> encodingTable = new Dictionary<byte, string>();

        public byte[] Compress(byte[] input)
        {
            // Count frequency of each byte
            var frequencies = new Dictionary<byte, int>();
            foreach (var b in input)
            {
                if (!frequencies.ContainsKey(b))
                    frequencies[b] = 0;
                frequencies[b]++;
            }

            // Create priority queue for building the Huffman tree
            var priorityQueue = new List<Node>();
            foreach (var kvp in frequencies)
            {
                priorityQueue.Add(new Node { Symbol = kvp.Key, Frequency = kvp.Value });
            }
            priorityQueue.Sort((a, b) => a.Frequency - b.Frequency);

            // Build Huffman Tree
            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue[0];
                var right = priorityQueue[1];
                priorityQueue.RemoveRange(0, 2);
                var parent = new Node
                {
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };
                priorityQueue.Add(parent);
                priorityQueue.Sort((a, b) => a.Frequency - b.Frequency);
            }
            var root = priorityQueue[0];

            // Build encoding table using iterative tree traversal
            var stack = new Stack<(Node, string)>();
            stack.Push((root, ""));
            while (stack.Count > 0)
            {
                var (node, path) = stack.Pop();
                if (node.IsLeaf)
                {
                    encodingTable[node.Symbol] = path;
                }
                else
                {
                    if (node.Right != null)
                        stack.Push((node.Right, path + "1"));
                    if (node.Left != null)
                        stack.Push((node.Left, path + "0"));
                }
            }

            // Encode input data
            var encodedData = new StringBuilder();
            foreach (var b in input)
            {
                encodedData.Append(encodingTable[b]);
            }

            // Track the length of the encoded data in bits
            int bitLength = encodedData.Length;

            // Convert encoded string to byte array
            var bitList = new List<byte>();
            for (int i = 0; i < encodedData.Length; i += 8)
            {
                var byteString = encodedData.ToString(i, Math.Min(8, encodedData.Length - i)).PadRight(8, '0');
                bitList.Add(Convert.ToByte(byteString, 2));
            }

            // Create a final byte array with bit length information as a header
            using var ms = new MemoryStream();
            // Add the bit length as a 4-byte header (int32)
            ms.Write(BitConverter.GetBytes(bitLength), 0, 4);
            // Add the encoded data
            ms.Write([.. bitList], 0, bitList.Count);
            return ms.ToArray();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // Read bit length from the first 4 bytes
            int bitLength = BitConverter.ToInt32(compressedData, 0);

            // Convert compressed byte array to binary string, ignoring extra bits
            var binaryData = new StringBuilder();
            for (int i = 4; i < compressedData.Length; i++)
            {
                binaryData.Append(Convert.ToString(compressedData[i], 2).PadLeft(8, '0'));
            }
            binaryData.Length = bitLength; // Truncate to the actual bit length

            // Traverse Huffman Tree to decode
            var result = new List<byte>();
            var root = BuildHuffmanTree();
            var currentNode = root;

            foreach (var bit in binaryData.ToString())
            {
                currentNode = bit == '0' ? currentNode.Left : currentNode.Right;
                if (currentNode!.IsLeaf)
                {
                    result.Add(currentNode.Symbol);
                    currentNode = root;
                }
            }

            return [.. result];
        }

        private Node BuildHuffmanTree()
        {
            // Rebuild the Huffman tree from encoding table (for decompression)
            var root = new Node();
            foreach (var kvp in encodingTable)
            {
                var currentNode = root;
                foreach (var bit in kvp.Value)
                {
                    if (bit == '0')
                    {
                        currentNode.Left ??= new Node();
                        currentNode = currentNode.Left;
                    }
                    else
                    {
                        currentNode.Right ??= new Node();
                        currentNode = currentNode.Right;
                    }
                }
                currentNode.Symbol = kvp.Key;
            }
            return root;
        }
    }
}