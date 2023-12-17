using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.DatabaseHelpers
{
    /*
    * Author   : Dušan Slúka (xsluka00)
    * File     : DatabaseConnection
    * Brief    : A utility class to manage SQLite database connections for the Connect4 application.
    *            This class provides a centralized way to obtain a new SQLiteConnection using a predefined
    *            connection string. It ensures consistent connection parameters across the application.
    */

    internal class DatabaseConnection
    {
        private static string _connectionString;

        static DatabaseConnection()
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Connect4",
                "Connect4.db");

            _connectionString = $"Data Source={dbPath};Version=3;";

            // Ensure the directory exists
            var dbDirectory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}
