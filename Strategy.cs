public interface ICompressionStrategy
{
    void Compress(string filePath);
}

public class ZipCompression : ICompressionStrategy
{
    public void Compress(string filePath)
    {
        Console.WriteLine($"Compressing {filePath} using ZIP compression.");
    }
}

public class RarCompression : ICompressionStrategy
{
    public void Compress(string filePath)
    {
        Console.WriteLine($"Compressing {filePath} using RAR compression.");
    }
}

public class Compressor
{
    private ICompressionStrategy _compressionStrategy;

    public Compressor(ICompressionStrategy compressionStrategy)
    {
        _compressionStrategy = compressionStrategy;
    }

    public void CompressFile(string filePath)
    {
        _compressionStrategy.Compress(filePath);
    }
}