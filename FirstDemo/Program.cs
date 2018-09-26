using System;
using System.Threading.Tasks;

namespace FirstDemo
{
    class Program
    {
        public static void Write(char c)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(c);
            }
        }

        static void Main(string[] args)
        {
            // Making new task vairabe and start it.
            Task.Factory.StartNew(() => Write('.'));

            var t = new Task(() => Write('?'));
            t.Start();

            Write('-');

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
