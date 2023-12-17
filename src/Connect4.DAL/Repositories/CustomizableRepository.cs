﻿using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : CustomizableRepository
 * Brief    : Implements functions for interaction with database for Customizables model
 */


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

        public ObservableCollection<string> GetCustomizablesForUser(int userId, int Own, int Type)
        {
            ObservableCollection<string> imagePathCollection = new ObservableCollection<string>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                SELECT C.ImagePath
                FROM Customizables C
                JOIN UserCustomizables UC ON C.Id = UC.CustomizableId
                WHERE UC.UserId = @UserId AND UC.Ownership = @Own AND C.Type = @Type;
                ";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);
                    cmd.Parameters.AddWithValue("Own", Own);
                    cmd.Parameters.AddWithValue("Type", Type);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string imagePath = reader["ImagePath"].ToString();
                            imagePathCollection.Add(imagePath);
                        }
                    }
                }
            }

            return imagePathCollection;
        }

        public ObservableCollection<string> GetCustomizablesPurch(int userId, int Own, int Type)
        {
            ObservableCollection<string> imagePathCollection = new ObservableCollection<string>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                SELECT C.ImagePath
                FROM Customizables C
                JOIN UserCustomizables UC ON C.Id = UC.CustomizableId
                WHERE UC.UserId = @UserId AND UC.Ownership = @Own AND C.Type = @Type;
                ";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);
                    cmd.Parameters.AddWithValue("Own", Own);
                    cmd.Parameters.AddWithValue("Type", Type);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string imagePath = reader["ImagePath"].ToString();
                            imagePathCollection.Add(imagePath);
                        }
                    }
                }
            }

            return imagePathCollection;
        }

        public ObservableCollection<string> GetAvailableCustomizables(int userId, int type)
        {
            ObservableCollection<string> imagePathCollection = new ObservableCollection<string>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = @"
                SELECT C.ImagePath
                FROM Customizables C
                LEFT JOIN UserCustomizables UC ON C.Id = UC.CustomizableId AND UC.UserId = @UserId
                WHERE UC.UserId IS NULL AND C.Type = @Type;
                ";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);
                    cmd.Parameters.AddWithValue("Type", type);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string imagePath = reader["ImagePath"].ToString();
                            imagePathCollection.Add(imagePath);
                        }
                    }
                }

            }

            return imagePathCollection;
        }
    }
}
