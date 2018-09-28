using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
            {
                Console.WriteLine("Cancelation has been requested");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancelation was requested");
            });

            Console.ReadKey();
            cts.Cancel();
            Console.ReadKey();
        }
    }
}
