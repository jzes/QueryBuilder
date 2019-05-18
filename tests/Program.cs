using System;
using System.Collections.Generic;
using System.Data.Common;
using ModelSQLBuilder;
using Npgsql;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var johnDoe = new People();
            johnDoe.Id = 156;
            johnDoe.FirstName = "John";
            johnDoe.Document = "12345678";
            johnDoe.CPF = "123456";
            johnDoe.BirdDate = DateTime.Now;
            johnDoe.Value = 2300.50;

            System.Console.WriteLine(johnDoe.BuildSelect());
            System.Console.WriteLine(johnDoe.BuildUpdate());
            System.Console.WriteLine(johnDoe.BuildWhereByKey());
            System.Console.WriteLine(johnDoe.BuildInsert());
            System.Console.WriteLine(johnDoe.BuildDelete());
        }
    }
}
