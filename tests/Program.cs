using System;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var johnDoe = new People();
            johnDoe.FirstName = "John";
            johnDoe.LastName = "Doe";
            johnDoe.Age = 28;
            johnDoe.BirdDate = DateTime.Now;
            johnDoe.Value = 2300.50;

            System.Console.WriteLine(johnDoe.BuildInsert());
            System.Console.WriteLine("\n---\n");
            System.Console.WriteLine(johnDoe.BuildUpdate());
            System.Console.WriteLine("\n---\n");
            System.Console.WriteLine(johnDoe.BuildSelect());
            System.Console.WriteLine("\n---\n");
            System.Console.WriteLine(johnDoe.BuildDelete());
        }
    }
}
