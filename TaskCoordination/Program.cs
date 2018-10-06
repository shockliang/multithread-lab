using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.Wait();
                Console.WriteLine("Here is your tea");
            });

            makeTea.Wait();

            Console.ReadKey();
        }
    }
}
