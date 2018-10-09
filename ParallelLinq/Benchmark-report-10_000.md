``` ini

BenchmarkDotNet=v0.11.1, OS=macOS Mojave 10.14 (18A391) [Darwin 18.0.0]
Intel Core i7-4770HQ CPU 2.20GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|               Method |     Mean |     Error |    StdDev |
|--------------------- |---------:|----------:|----------:|
|           RegularSum | 63.31 us | 0.4176 us | 0.3702 us |
|         AggregateSum | 70.83 us | 0.3528 us | 0.3300 us |
| ParallelAggregateSum | 41.00 us | 0.7702 us | 0.7909 us |
