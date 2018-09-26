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

        public static void Write(object o)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(o);
            }
        }

        static void Main(string[] args)
        {
            var t = new Task(Write, "hello");
            t.Start();

            Task.Factory.StartNew(Write, 123);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
