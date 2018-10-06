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
            var evt = new AutoResetEvent(false); // Defautl: false

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set(); // set to true
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.WaitOne(); // set to false and false
                Console.WriteLine("Here is your tea");
                var ok = evt.WaitOne(1000); // set to false with timeout
                if(ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else 
                {
                    // Because no one to set to true so it blocked 
                    Console.WriteLine("No tea for you"); 
                }
            });

            makeTea.Wait();

            Console.ReadKey();
        }
    }
}
