using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTimeToPass
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                // Thread.Sleep(1000);
                // Thread.SpinWait(1000);
                // SpinWait.SpinUntil(() => true);

                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                var cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Bomb disarmed." : "BOOM!!");
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();
            Console.ReadKey();

        }
    }
}
