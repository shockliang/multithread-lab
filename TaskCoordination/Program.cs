using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting.");
                    Thread.Sleep(3000);
                    // throw new Exception();
                    Console.WriteLine("Child task finshing.");
                }, TaskCreationOptions.AttachedToParent);

                var completionHandle = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Horray! Task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent |
                    TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops! task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent |
                    TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });
            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
            Console.ReadKey();
        }
    }
}
