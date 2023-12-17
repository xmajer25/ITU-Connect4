using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : AchievementRepository
 * Brief    : Implements functions for interaction with database for Achievements model
 */


namespace Connect4.DAL.Repositories
{
    public class AchievementRepository
    {
        public List<Achievement> GetAllAchievements()
        {
            List<Achievement> achievements = new List<Achievement>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Achievements";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            achievements.Add(new Achievement
                            {
                                AchievementId = Convert.ToInt32(reader["AchievementId"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
            }

            return achievements;
        }

        public Achievement GetAchievementById(int achievementId)
        {
            Achievement achievement = null;

            string query = "SELECT * FROM Achievements WHERE AchievementId = @AchievementId";

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("AchievementId", achievementId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            achievement = new Achievement
                            {
                                AchievementId = Convert.ToInt32(reader["AchievementId"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }

            return achievement;
        }
    }
}
