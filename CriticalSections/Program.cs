using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CriticalSections
{
    class BankAccount
    {
        private int balance;
        public int Balance
        {
            get { return balance; }
            private set { balance = value; }
        }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
        }

        public void Tranfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }

    class Program
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();
        static Random random = new Random();

        static void Main(string[] args)
        {
            int x = 0;

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    // padlock.EnterReadLock();
                    padlock.EnterUpgradeableReadLock();

                    if (i % 2 == 0)
                    {
                        padlock.EnterWriteLock();
                        x = 1234;
                        padlock.ExitWriteLock();
                    }

                    Console.WriteLine($"Entered read lock x = {x}");
                    Thread.Sleep(5000);

                    padlock.ExitUpgradeableReadLock();
                    // padlock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write lock acquired");

                x = random.Next(10);
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }
        }
    }
}
