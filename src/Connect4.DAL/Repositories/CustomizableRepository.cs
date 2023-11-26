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
    public class CustomizableRepository
    {
        public List<Customizable> GetAllCustomizables()
        {
            List<Customizable> customizables = new List<Customizable>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Customizables";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customizables.Add(new Customizable
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                IsToken = Convert.ToBoolean(reader["IsToken"]),
                                IsAvatar = Convert.ToBoolean(reader["IsAvatar"]),
                                IsBack = Convert.ToBoolean(reader["IsBack"])
                            });
                        }
                    }
                }
            }

            return customizables;
        }

        public Customizable GetCustomizableById(int id)
        {
            Customizable customizable = null;

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Customizables WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("Id", id);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customizable = new Customizable
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ImagePath = reader["ImagePath"].ToString(),
                                IsToken = Convert.ToBoolean(reader["IsToken"]),
                                IsAvatar = Convert.ToBoolean(reader["IsAvatar"]),
                                IsBack = Convert.ToBoolean(reader["IsBack"]),
                            };
                        }
                    }
                }
            }

            return customizable;
        }
    }
}
