using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            if (stack.TryPeek(out int result))
            {
                Console.WriteLine($"{result} is on the top");
            }

            if (stack.TryPop(out result))
            {
                Console.WriteLine($"Popped {result}");
            }

            var itmes = new int[5];
            if (stack.TryPopRange(itmes) > 0)
            {
                var text = String.Join(", ", itmes.Select(i => i.ToString()));
                Console.WriteLine($"Popped these items: {text}");
            }
        }
    }
}
