namespace Compressors
{
    public interface ICompressor
    {
        public byte[] Compress(byte[] data);
        public byte[] Decompress(byte[] encodedData);
    }
}