using Connect4.DAL.DatabaseHelpers;
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
                connection.Open();
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

        public int GetIdByImagePath(string imagePath)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT Id FROM Customizables WHERE ImagePath = @ImagePath";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        return id;
                    }
                }
            }
            return -1;
        }
    }
}
