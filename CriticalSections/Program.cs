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
    }

    class Program
    {
        private static SpinLock sl = new SpinLock(true);

        static void Main(string[] args)
        {
            LockRecursion(5);

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static void LockRecursion(int x)
        {
            var lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
    }
}
