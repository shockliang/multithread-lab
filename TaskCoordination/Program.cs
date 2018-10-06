using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    class Program
    {
        private static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait(); // step2. counter = 2. 
            Console.WriteLine("Pouring water into cup"); // step3. counter = 0. reset the counter.
            barrier.SignalAndWait(); // step4. counter = 1 
            Console.WriteLine("Putting the kettle away!");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            barrier.SignalAndWait(); // step1. counter = 1
            Console.WriteLine("Adding tea.");
            barrier.SignalAndWait(); // step5. counter = 2
            Console.WriteLine("Adding sugar.");
        }

        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea.");
            });

            tea.Wait();

            Console.ReadKey();
        }
    }
}
