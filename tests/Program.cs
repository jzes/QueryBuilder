using System;
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
            // johnDoe.Id = 156;
            // johnDoe.FirstName = "John";
            // johnDoe.Documento = "12345678";
            // johnDoe.CPF = "123456";
            // johnDoe.BirdDate = DateTime.Now;
            // johnDoe.Value = 2300.50;

            var dataAccess = new DataAccess(new NpgsqlConnection(), "Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=2345");
            // dataAccess.ExecuteVoidSQL(new NpgsqlCommand(johnDoe.BuildInsert()));

            dataAccess.ExecuteSQLStrategy(connection => {
                var select = new NpgsqlCommand(johnDoe.BuildSelect(), (NpgsqlConnection)connection);
                var data = select.ExecuteReader();
                johnDoe.GetOne(data);
            });
            System.Console.WriteLine(johnDoe.FirstName);
        }
    }
}
