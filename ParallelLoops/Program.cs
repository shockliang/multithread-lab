using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ParallelLoops
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            Console.WriteLine(summary);
            // Console.ReadKey();
        }

        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 1_000_000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            // Benchmark takes 26.72 ms
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
        }

        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 1_000_000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            // Benchmark takes 7.807 ms
            var part = Partitioner.Create(0, count, 100_000);
            Parallel.ForEach(part, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int)Math.Pow(i, 2);
                }
            });
        }
    }
}
