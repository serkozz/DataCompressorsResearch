```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4169/23H2/2023Update/SunValley3)
Intel Core i5-8300H CPU 2.30GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-rc.2.24474.11
  [Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2


```
| Method                             | Mean      | Error     | StdDev    |
|----------------------------------- |----------:|----------:|----------:|
| LZW_Compression_HelloWorldString   |  7.768 ms | 0.1549 ms | 0.3970 ms |
| LZW_Compression_VeryLongString     |  9.624 ms | 0.1885 ms | 0.3045 ms |
| LZW_Decompression_HelloWorldString |  8.944 ms | 0.1781 ms | 0.4785 ms |
| LZW_Decompression_VeryLongString   | 16.324 ms | 0.3439 ms | 1.0141 ms |
