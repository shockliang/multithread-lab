using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            if (q.TryDequeue(out int result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Front element is {result}");
            }

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Enqueue:{i}");
                    q.Enqueue(i);
                }));
            }

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    if (q.TryDequeue(out int element))
                    {
                        Console.WriteLine($"Try dequeue:{element}");
                    }
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
