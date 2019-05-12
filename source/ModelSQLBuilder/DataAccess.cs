using System;
using System.Data.Common;

namespace ModelSQLBuilder
{
    public class DataAccess
    {
        private DbConnection connection;
        private string connectionString;
        public DataAccess(DbConnection connection, string connectionString){
            this.connection = connection;
            this.connectionString = connectionString;
        }

        private void MakeConnection(){
            connection = (DbConnection)Activator.CreateInstance(connection.GetType());
            connection.ConnectionString = connectionString;
        }

        public void ExecuteVoidSQL(DbCommand command){
            MakeConnection();
            using(connection){
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }

        public void ExecuteSQLStrategy(Action<DbConnection> strategy){
            MakeConnection();
            using(connection){
                connection.Open();
                strategy(connection);
            }
        }
    }
}