using System.Collections;

namespace Compressors;

public class HuffmanCompresor
{
    private class HuffmanNode
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode? Left { get; set; }
        public HuffmanNode? Right { get; set; }

        public List<bool>? Traverse(char symbol, List<bool> data)
        {
            if (Right == null && Left == null)
            {
                if (symbol == Symbol)
                {
                    return data;
                }
                return null;            
            }
            else
            {
                List<bool>? leftPath = null, rightPath = null;
                if (Left != null)
                {
                    List<bool> left = new(data) { false };
                    leftPath = Left?.Traverse(symbol, left);
                }
                if (Right != null)
                {
                    List<bool> right = new(data) { true };
                    rightPath = Right?.Traverse(symbol, right);
                }
                return leftPath ?? rightPath;
            }
        }
    }

    private readonly List<HuffmanNode> Nodes = [];
    private HuffmanNode? Root { get; set; }
    private readonly Dictionary<char, int> Frequencies = [];

    public void Build(string source)
    {
        // Calculate frequency of each symbol
        for (int i = 0; i < source.Length; i++)
        {
            if (!Frequencies.TryGetValue(source[i], out int value))
            {
                value = 0;
                Frequencies[source[i]] = value;
            }
            Frequencies[source[i]] = ++value;
        }

        // Build nodes from frequencies
        foreach (var symbol in Frequencies)
        {
            Nodes.Add(new HuffmanNode() { Symbol = symbol.Key, Frequency = symbol.Value });
        }

        // Build the tree
        while (Nodes.Count > 1)
        {
            List<HuffmanNode> orderedNodes = [.. Nodes.OrderBy(node => node.Frequency)];

            if (orderedNodes.Count >= 2)
            {
                // Take first two items
                HuffmanNode left = orderedNodes[0];
                HuffmanNode right = orderedNodes[1];

                // Create new node with these two
                HuffmanNode parent = new()
                {
                    Symbol = '*', // Placeholder symbol for non-leaf nodes
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                Nodes.Remove(left);
                Nodes.Remove(right);
                Nodes.Add(parent);
            }
            Root = Nodes.FirstOrDefault();
        }
    }

    public BitArray Encode(string source)
    {
        List<bool> encodedSource = [];

        for (int i = 0; i < source.Length; i++)
        {
            List<bool>? encodedSymbol = Root?.Traverse(source[i], []);
            encodedSource.AddRange(encodedSymbol!);
        }

        BitArray bits = new(encodedSource.ToArray());
        return bits;
    }

    public string Decode(BitArray bits)
    {
        HuffmanNode? current = Root;
        string decoded = "";

        foreach (bool bit in bits)
        {
            current = bit ? current?.Right : current?.Left;

            // If leaf node
            if (current?.Left == null && current?.Right == null)
            {
                decoded += current?.Symbol;
                current = Root;
            }
        }
        return decoded;
    }
}