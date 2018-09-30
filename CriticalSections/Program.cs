using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CriticalSections
{
    class BankAccount
    {
        private object padlock = new object();
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            // += 
            // op1: tmp <- get_Balance() + amount
            // op2: set_Balance(tmp)
            lock (padlock)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
