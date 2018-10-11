using System;
using System.Threading.Tasks;

namespace AsyncAndAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var t = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(3000);
                return 123;
            }).Unwrap();

            Console.WriteLine($"Task.Factory.StartNew result :{t.Result}");

            int result = await Task.Run(async () =>
            {
                await Task.Delay(3000);
                return 456;
            });

            Console.WriteLine($"Task.Run result:{result}");
        }
    }
}
