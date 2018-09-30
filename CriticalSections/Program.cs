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
        private static SpinLock sl = new SpinLock(true);

        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            var mutex = new Mutex();
            var mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock) mutex2.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Tranfer(ba2, 1);
                        }
                        finally
                        {
                            if(haveLock) 
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance 1 is {ba.Balance}");
            Console.WriteLine($"Final balance 2 is {ba2.Balance}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
