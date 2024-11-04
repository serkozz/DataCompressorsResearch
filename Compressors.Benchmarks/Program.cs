using BenchmarkDotNet.Running;
using Compressors.Benchmarks;

BenchmarkRunner.Run([typeof(HuffmanBenchmarks), typeof(LZWBenchmarks)]);