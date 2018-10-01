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
        private static Mutex mutex;
        static void Main(string[] args)
        {
            const string appName = "MyApp";
            var hasBeenCreated = false;
            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("We can run the program just fine.");
                mutex = new Mutex(false, appName, out hasBeenCreated);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            Console.ReadKey();
            if(hasBeenCreated)
                mutex.ReleaseMutex();
        }
    }
}
