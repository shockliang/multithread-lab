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
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine(oce.Message);
            }
            // Console.ReadKey();
        }

        public static void Demo()
        {
            var cts = new CancellationTokenSource();
            var po = new ParallelOptions()
            {
                CancellationToken = cts.Token
            };

            var result = Parallel.For(0, 20, po, (x, state) =>
             {
                 Console.WriteLine($"{x}[{Task.CurrentId}]");

                 if (x == 10)
                 {
                    // throw new Exception();
                    // state.Stop();   // Stop loop
                    // state.Break();
                    cts.Cancel();
                 }
             });

            Console.WriteLine();
            Console.WriteLine($"Was loop completed? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
            }
        }
    }
}
