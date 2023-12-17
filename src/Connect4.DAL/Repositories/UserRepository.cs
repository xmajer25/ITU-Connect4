using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut(xmahut01)
 * File     : User
 * Brief    : Data model to store User information
 */

namespace Connect4.DAL.Repositories
{
    public class UserRepository
    {
        public void CreateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, Password, Email, GamesPlayed, GamesWon, GoldTotal, GoldActual)" +
                    " VALUES (@Username, @Password, @Email, @GamesPlayed, @GamesWon, @GoldTotal, @GoldActual)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("GamesPlayed", user.GamesPlayed);
                    command.Parameters.AddWithValue("GamesWon", user.GamesWon);
                    command.Parameters.AddWithValue("GoldTotal", user.GoldTotal);
                    command.Parameters.AddWithValue("GoldActual", user.GoldActual);

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

        public List<string> GetAllEmails()
        {
            List<string> emails = new List<string>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT Email FROM Users";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            emails.Add(reader["Email"].ToString());
                        }
                    }
                }
            }

            return emails;
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
                                Email = reader["Email"].ToString(),
                                GamesPlayed = Convert.ToInt32(reader["GamesPlayed"]),
                                GamesWon = Convert.ToInt32(reader["GamesWon"]),
                                GoldTotal = Convert.ToInt32(reader["GoldTotal"]),
                                GoldActual = Convert.ToInt32(reader["GoldActual"])
                            };
                        }
                    }
                }
            }

            return null; // Return null if no user was found
        }

        public void UpdateGoldActual(int userId, int newGoldActual)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string updateQuery = "UPDATE Users SET GoldActual = @NewGoldActual WHERE Id = @UserId";

                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@NewGoldActual", newGoldActual);

                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetGoldActual(int userId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT GoldActual FROM Users WHERE Id = @UserId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int goldActual))
                    {
                        return goldActual;
                    }
                }
            }

            // Default value or handle the case where the user is not found
            throw new InvalidOperationException($"Unable to retrieve GoldActual for user with ID {userId}");
        }

        public void UpdateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE Users SET Username = @Username, Password = @Password, Email = @Email, GamesPlayed = @GamesPlayed, GamesWon = @GamesWon, GoldTotal = @GoldTotal, GoldActual = @GoldActual WHERE Id = @UserId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("UserId", user.Id);
                    command.Parameters.AddWithValue("Username", user.Username);
                    command.Parameters.AddWithValue("Password", user.Password);
                    command.Parameters.AddWithValue("Email", user.Email);
                    command.Parameters.AddWithValue("GamesPlayed", user.GamesPlayed);
                    command.Parameters.AddWithValue("GamesWon", user.GamesWon);
                    command.Parameters.AddWithValue("GoldTotal", user.GoldTotal);
                    command.Parameters.AddWithValue("GoldActual", user.GoldActual);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
