using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            var counter = 0;
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(Interlocked.Increment(ref counter));
                    Console.WriteLine($"Task - {Task.CurrentId} has added {counter}");

                    if(bag.TryPeek(out int result))
                    {
                        Console.WriteLine($"Task - {Task.CurrentId} has peeked the value {result}");
                    }
                    
                }));
            }

            Task.WaitAll(tasks.ToArray());

            if(bag.TryTake(out int last))
            {
                Console.WriteLine($"Got the {last}");
            }
        }
    }
}
