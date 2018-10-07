using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;

            Parallel.For(1, 1001,
            () => 0,
            (x, state, tls) =>
            {
                tls += x;
                Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                return tls;
            },
            partialSum =>
            {
                Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                Interlocked.Add(ref sum, partialSum);
            });
            Console.WriteLine($"Sum of 1 to 1000 = {sum}");
            // Console.ReadKey();
        }
    }
}
