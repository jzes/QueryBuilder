using System;
using System.Collections.Generic;
using System.Data.Common;
using Npgsql;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("+++++++++\nInsert a new row\n");
            var newPeople = new People();
            newPeople.Name = "Josef Paul";
            newPeople.Role = "Manager";
            newPeople.Document = "125344321";
            newPeople.CPF = "345345";
            newPeople.BirthDate = DateTime.Parse("1989-03-05");

            var connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=123");
            using(connection){
                connection.Open();
                var command = new NpgsqlCommand(newPeople.BuildInsert(), connection);
                System.Console.WriteLine(newPeople.BuildInsert());
                command.ExecuteNonQuery();
            }

            System.Console.WriteLine("+++++++++\nGet Many rows with manual where\n");
            connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=123");
            var people = new People();
            var peoples = new List<People>();
            using(connection){
                connection.Open();
                var command = new NpgsqlCommand(people.BuildSelect()+"where nome_str = 'Jose'", connection);
                var data = command.ExecuteReader();
                peoples = people.GetManyFromDataReader(data);
            }
            foreach(var p in peoples){
                System.Console.WriteLine(p.Name);
                System.Console.WriteLine(p.Role);
                System.Console.WriteLine(p.CPF);
                System.Console.WriteLine(p.BirthDate.ToString("dd/MM/yyyy"));
                System.Console.WriteLine("------");
            }

            System.Console.WriteLine("+++++++++\nGet one row with auto where\n");
            var johnDoe = new People();
            johnDoe.Id = 1;
            connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=123");
            using (connection)
            {
                connection.Open();
                var command = new NpgsqlCommand(johnDoe.BuildSelect() + johnDoe.BuildWhereByKey(), connection);
                var data = command.ExecuteReader();
                if (data.Read())
                {
                    johnDoe.GetOneFromDataReader(data);
                }
            }
            System.Console.WriteLine(johnDoe.Name);
            System.Console.WriteLine(johnDoe.Role);
            System.Console.WriteLine(johnDoe.BirthDate.ToString("dd/MM/yyyy"));

            System.Console.WriteLine("+++++++++\nUpdate one\n");
            johnDoe.Role = "CEO";
            connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=123");
            using (connection)
            {
                connection.Open();
                var command = new NpgsqlCommand(johnDoe.BuildUpdate(), connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
