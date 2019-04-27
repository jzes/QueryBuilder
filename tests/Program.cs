using System;
using System.Data.Common;
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
            // johnDoe.LastName = "John";
            // johnDoe.Contrato = "123456";
            // johnDoe.Age = 28;
            // johnDoe.BirdDate = DateTime.Now;
            // johnDoe.Value = 2300.50;

            // System.Console.WriteLine(johnDoe.BuildInsert());
            // System.Console.WriteLine("\n---\n");
            // System.Console.WriteLine(johnDoe.BuildUpdate());
            // System.Console.WriteLine("\n---\n");
            // System.Console.WriteLine(johnDoe.BuildSelect()+johnDoe.BuildWhereByKey());
            // System.Console.WriteLine("\n---\n");
            // System.Console.WriteLine(johnDoe.BuildDelete());
            
            var con = new NpgsqlConnection("");
            
            using(con){
                con.Open();
                var comm = new NpgsqlCommand("select * from pessoas limit 10", con);
                DbDataReader data = comm.ExecuteReader();
                while(data.Read()){
                    johnDoe.GetUm(data);
                    System.Console.WriteLine(johnDoe.Contrato);
                    System.Console.WriteLine(johnDoe.FirstName);
                }
            }
        }
    }
}
