```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4317/23H2/2023Update/SunValley3)
Intel Core i5-8300H CPU 2.30GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-rc.2.24474.11
  [Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2


```
| Method                                 | Mean         | Error      | StdDev     |
|--------------------------------------- |-------------:|-----------:|-----------:|
| Huffman_Compression_HelloWorldString   |     1.624 μs |  0.0344 μs |  0.0969 μs |
| Huffman_Compression_VeryLongString     | 1,050.744 μs | 18.8137 μs | 17.5984 μs |
| Huffman_Decompression_HelloWorldString |           NA |         NA |         NA |
| Huffman_Decompression_VeryLongString   |           NA |         NA |         NA |

Benchmarks with issues:
  HuffmanBenchmarks.Huffman_Decompression_HelloWorldString: DefaultJob
  HuffmanBenchmarks.Huffman_Decompression_VeryLongString: DefaultJob
