using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        static BlockingCollection<int> messages =
            new BlockingCollection<int>(new ConcurrentBag<int>(), 5);

        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);
            Console.ReadKey();
            cts.Cancel();
        }
        static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, cts.Token);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.InnerException);
                ae.Handle(e => true);
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+ {i}\t");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"- {item}\t");
                Thread.Sleep(random.Next(1000));
            }
        }
    }
}
