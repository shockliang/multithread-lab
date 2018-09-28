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
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }
                    else
                        Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();
            Console.ReadKey();
        }
    }
}
