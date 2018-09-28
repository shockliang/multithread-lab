using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var planned = new CancellationTokenSource();
            var prventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, prventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(100);
                }
            }, paranoid.Token);


            Console.ReadKey();
            emergency.Cancel();
            Console.ReadKey();
        }
    }
}
