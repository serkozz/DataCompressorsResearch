```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4169/23H2/2023Update/SunValley3)
Intel Core i5-8300H CPU 2.30GHz (Coffee Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.100-rc.2.24474.11
  [Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2


```
| Method                   | Mean           | Error       | StdDev      | Median         |
|------------------------- |---------------:|------------:|------------:|---------------:|
| Huffman_HelloWorldString |       7.262 μs |   0.1432 μs |   0.2619 μs |       7.140 μs |
| Huffman_VeryLongString   | 102,553.955 μs | 778.3388 μs | 728.0586 μs | 102,559.540 μs |
