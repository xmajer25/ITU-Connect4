using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.DatabaseHelpers
{
    internal class DatabaseConnection
    {
        private static string _connectionString = "Data Source=YourDatabaseName.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}
