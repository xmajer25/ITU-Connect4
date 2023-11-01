using Connect4.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.DatabaseHelpers
{
    public class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Create the table if it doesn't exist
                string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE
            )";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert dummy values if no users exist yet
                UserRepository userRepository = new UserRepository();
                if (!userRepository.AnyUsersExist())
                {
                    InsertDummyUsers(connection);
                }
            }
        }

        private static void InsertDummyUsers(SQLiteConnection connection)
        {
            string insertDummyQuery = @"
        INSERT INTO Users (Username, Password, Email) VALUES 
        ('DummyUser1', 'DummyPass1', 'dummy1@email.com'),
        ('DummyUser2', 'DummyPass2', 'dummy2@email.com'),
        ('DummyUser3', 'DummyPass3', 'dummy3@email.com')";

            using (var command = new SQLiteCommand(insertDummyQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
