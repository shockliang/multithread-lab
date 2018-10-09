using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 20).ToArray();
            var results = numbers
                .AsParallel()
                // .WithMergeOptions(ParallelMergeOptions.NotBuffered)  // Output result as soon as possible.
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)   // Output fully bunch result.
                .Select(x =>
                {
                    var result = Math.Log10(x);
                    Console.WriteLine($"P {result}\t");
                    return result;
                });

            foreach (var result in results)
            {
                Console.WriteLine($"C {result}\t");
            }
            Console.ReadKey();
        }
    }
}
