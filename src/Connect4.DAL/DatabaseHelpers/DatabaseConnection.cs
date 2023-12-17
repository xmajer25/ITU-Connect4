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
        private static string _databaseFileName = "Connect4.db";
        private static string _connectionString = $"Data Source={_databaseFileName};Version=3;";

        // Keep a static reference to the open connection
        private static SQLiteConnection _openConnection;

        public static SQLiteConnection GetConnection()
        {
            // Check if the database file exists
            if (!File.Exists(_databaseFileName))
            {
                // If the database file doesn't exist, create it
                SQLiteConnection.CreateFile(_databaseFileName);

                // Optionally, you may want to execute initial database setup scripts here

                Console.WriteLine("Database file created successfully.");
            }

            return new SQLiteConnection(_connectionString);
        }
    }
}