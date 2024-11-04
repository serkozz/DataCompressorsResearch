using System.Text;

namespace Compressors.Tests;

public class HuffmanTests
{
    [Theory, Repeat(100)]
    public void EnglishASCIICompatible_ShouldCompressWith1_2RatioAtLeast(int count)
    {
        StringBuilder sb = new(100_000);
        foreach (char c in Enumerable.Range(0, 100_000).Select((i) => (char)Random.Shared.Next(97, 123)))
        {
            sb.Append(c);
        }

        var testString = sb.ToString();

        //Arrange
        var data = Encoding.UTF8.GetBytes(testString!);

        var compressor = new HuffmanCompressor();

        var encodedData = compressor.Compress(data);

        // Decode data
        var decodedData = compressor.Decompress(encodedData);
        var decodedString = Encoding.UTF8.GetString(decodedData);

        var compressionRatio = data.Length / (float)encodedData.Length;

        Assert.Equal(testString, decodedString);
        Assert.True(compressionRatio > 1.2f);
    }

    [Theory, Repeat(100)]
    public void AllASCIICompatible_ShouldCompressWith1_2RatioAtLeast(int count)
    {
        StringBuilder sb = new(100_000);
        foreach (char c in Enumerable.Range(0, 100_000).Select((i) => (char)Random.Shared.Next(32, 127)))
        {
            sb.Append(c);
        }

        var testString = sb.ToString();

        //Arrange
        var data = Encoding.UTF8.GetBytes(testString!);

        var compressor = new HuffmanCompressor();

        var encodedData = compressor.Compress(data);

        // Decode data
        var decodedData = compressor.Decompress(encodedData);
        var decodedString = Encoding.UTF8.GetString(decodedData);

        var compressionRatio = data.Length / (float)encodedData.Length;

        Assert.True(testString == decodedString && compressionRatio > 1.2f);
    }
}
