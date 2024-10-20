using System.Collections;

namespace Compressors.Tests;

public class HuffmanTests
{
    [Fact]
    public void HuffmanCompressor_EncodedAndDecodedStringsAreEquals()
    {
        //Arrange
        HuffmanCompresor compresor = new();
        string testString = "Hello world";
        BitArray? encodedBitsArray;
        string decodedString = string.Empty;

        //Act
        compresor.Build(testString);
        encodedBitsArray = compresor.Encode(testString);
        decodedString = compresor.Decode(encodedBitsArray);

        //Assert
        Assert.Equal(testString, decodedString);
    }
}
