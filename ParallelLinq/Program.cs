using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ParallelLinq
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            Console.WriteLine(summary);
            // var p = new Program();
            // p.RegularSum();
            // p.AggregateSum();
            // p.ParallelAggregateSum();
        }

        [Benchmark]
        public void RegularSum()
        {
            var sum = Enumerable.Range(1, 10_000).Sum();
            // Console.WriteLine($"Regular sum {sum}");
        }

        [Benchmark]
        public void AggregateSum()
        {
            var sum = Enumerable.Range(1, 10_000)
                .Aggregate(0, (i, acc) => i + acc);
            // Console.WriteLine($"Aggregate sum {sum}");
        }

        [Benchmark]
        public void ParallelAggregateSum()
        {
            var sum = ParallelEnumerable.Range(1, 10_000)
                .Aggregate(
                    0,
                    (partialSum, i) => partialSum += i,
                    (total, subTotal) => total += subTotal,
                    i => i);
            // Console.WriteLine($"Parallel aggregate sum {sum}");
        }
    }
}
