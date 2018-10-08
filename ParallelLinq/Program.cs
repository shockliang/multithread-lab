using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            const int count = 50;
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });

            Console.WriteLine();

            // foreach (var i in results)
            // {
            //     Console.WriteLine($"{i}\t");
            // }

            // Console.WriteLine();

            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);
            foreach(var i in cubes)
            {
                Console.Write($"{i}\t");
            }

            Console.ReadKey();
        }
    }
}
