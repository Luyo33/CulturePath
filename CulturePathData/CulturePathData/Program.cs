using System;

namespace CulturePathData
{
    class Program
    {
        static void Main(string[] args)
        {
            if (User.AddUser("luyo33luyo@gmail.com", "Luyo", "0123", "Jack", "Cheninade", 66))
                Console.WriteLine("Done");
            else
                Console.WriteLine("Easy");
            Console.ReadKey();
        }
    }
}
