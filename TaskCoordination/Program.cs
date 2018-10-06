using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    class Program
    {
        private static int taskCount = 5;
        private static Random random = new Random();
        static CountdownEvent cte = new CountdownEvent(taskCount);

        static void Main(string[] args)
        {
            for (int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to comlete in {Task.CurrentId}");
                cte.Wait();
                Console.WriteLine("All tasks completed");
            });

            finalTask.Wait();
            Console.ReadKey();
        }
    }
}
