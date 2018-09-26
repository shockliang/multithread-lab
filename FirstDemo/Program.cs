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

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }

        static void Main(string[] args)
        {
            string text1 = "testing", text2 = "this";

            var task1 = new Task<int>(TextLength, text1);
            task1.Start();

            var task2 = Task.Factory.StartNew<int>(TextLength, text2);

            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");

            Console.ReadKey();
        }
    }
}
