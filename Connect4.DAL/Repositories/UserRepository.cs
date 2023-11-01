using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.Repositories
{
    public class UserRepository
    {
        public void CreateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool AnyUsersExist()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT EXISTS(SELECT 1 FROM Users LIMIT 1)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    return Convert.ToBoolean(command.ExecuteScalar());
                }
            }
        }

        public List<string> GetAllUsernames()
        {
            List<string> usernames = new List<string>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT Username FROM Users";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usernames.Add(reader["Username"].ToString());
                        }
                    }
                }
            }

            return usernames;
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
            }

            return null; // Return null if no user was found
        }

    }
}
