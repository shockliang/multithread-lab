using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t1 = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I'm done after 5 seconds");
            }, token);
            t1.Start();

            var t2 = Task.Factory.StartNew(() =>
            {
                token.ThrowIfCancellationRequested();
                Console.WriteLine("I take 3 seconds");
                Thread.Sleep(3000);
                Console.WriteLine("I'm done after 3 seconds");

            }, token);

            // Done after 5 seconds
            // Task.WaitAll(t1, t2);

            // Done after 3 sec
            // Task.WaitAny(t1, t2);

            // Only waiting for 4 seconds. 
            Task.WaitAny(new[] { t1, t2 }, 4000, token);

            Console.WriteLine($"Task t1 status is {t1.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
