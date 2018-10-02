using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        private static ConcurrentDictionary<string, string> capitals =
            new ConcurrentDictionary<string, string>();
        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis);
            AddParis();

            capitals["Russia"] = "Leningrad";
            capitals.AddOrUpdate("Russia", "Moscow", (key, old) => old + " --> Moscow");
            Console.WriteLine($"The capital of Russia is {capitals["Russia"]}");

            // capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capOfSweden}");

            const string toRemove = "Russia";
            var didRemove = capitals.TryRemove(toRemove, out string removed);
            if (didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove the capital of {toRemove}");
            }

            foreach (var kvp in capitals)
            {
                Console.WriteLine($" - {kvp.Value} is the capital of {kvp.Key}");
            }
        }

        static void AddParis()
        {
            var success = capitals.TryAdd("France", "Paris");
            var who = Task.CurrentId.HasValue ? $"Task :{Task.CurrentId}" : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
        }
    }
}
