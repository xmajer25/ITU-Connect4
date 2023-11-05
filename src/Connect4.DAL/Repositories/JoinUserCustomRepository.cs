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
    public class JoinUserCustomRepository
    {
        public void Create(JoinUserCustom userCustomizable)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserCustomizables (UserId, CustomizableId, Ownership) VALUES (@UserId, @CustomizableId, @Ownership)", connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userCustomizable.UserId);
                    cmd.Parameters.AddWithValue("CustomizableId", userCustomizable.CustomizableId);
                    cmd.Parameters.AddWithValue("Ownership", userCustomizable.Ownership);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JoinUserCustom> GetUserCustomizablesByUserId(int userId)
        {
            List<JoinUserCustom> userCustomizables = new List<JoinUserCustom>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM UserCustomizables WHERE UserId = @UserId";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userCustomizables.Add(new JoinUserCustom
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                CustomizableId = Convert.ToInt32(reader["CustomizableId"]),
                                Ownership = Convert.ToInt32(reader["Ownership"])
                            });
                        }
                    }
                }
            }

            return userCustomizables;
        }

        public List<JoinUserCustom> GetUserCustomizablesByIsTokenAndUser(int userId)
        {
            List<JoinUserCustom> userCustomizables = new List<JoinUserCustom>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM UserCustomizables WHERE UserId = @UserId AND IsToken = 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userCustomizables.Add(new JoinUserCustom
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                CustomizableId = Convert.ToInt32(reader["CustomizableId"]),
                                Ownership = Convert.ToInt32(reader["Ownership"])
                            });
                        }
                    }
                }
            }

            return userCustomizables;
        }

        // Retrieve UserCustomizable records by IsAvatar flag and UserId
        public List<JoinUserCustom> GetUserCustomizablesByIsAvatarAndUser(int userId)
        {
            List<JoinUserCustom> userCustomizables = new List<JoinUserCustom>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM UserCustomizables WHERE UserId = @UserId AND IsAvatar = 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userCustomizables.Add(new JoinUserCustom
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                CustomizableId = Convert.ToInt32(reader["CustomizableId"]),
                                Ownership = Convert.ToInt32(reader["Ownership"])
                            });
                        }
                    }
                }
            }

            return userCustomizables;
        }

        // Retrieve UserCustomizable records by IsBack flag and UserId
        public List<JoinUserCustom> GetUserCustomizablesByIsBackAndUser(int userId)
        {
            List<JoinUserCustom> userCustomizables = new List<JoinUserCustom>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM UserCustomizables WHERE UserId = @UserId AND IsBack = 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userCustomizables.Add(new JoinUserCustom
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                CustomizableId = Convert.ToInt32(reader["CustomizableId"]),
                                Ownership = Convert.ToInt32(reader["Ownership"])
                            });
                        }
                    }
                }
            }

            return userCustomizables;
        }
    }
}
