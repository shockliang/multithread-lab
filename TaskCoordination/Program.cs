using System;
using System.Threading.Tasks;

namespace TaskCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boling water");
            });

            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"completed task {t.Id}, pour water into cup.");
            });

            task2.Wait();

            Console.ReadKey();
        }
    }
}
