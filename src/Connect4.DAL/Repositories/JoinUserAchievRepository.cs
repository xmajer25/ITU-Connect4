using System;
using System.Collections.Generic;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : JoinUserAchievRepository
 * Brief    : Implements functions for interaction with database for Join table of User and Achievement
 */


namespace Connect4.DAL.Repositories
{
    public class JoinUserAchievRepository
    {
        public void CreateJoinUserAchieve(UserAchievement userAchievement)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO UserAchievements(UserId, AchievementId, Earned) VALUES(@UserId, @AchievementId, @Earned)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                    command.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                    command.Parameters.AddWithValue("@Earned", userAchievement.Earned);

                    command.ExecuteNonQuery();
                }
            }
        }


        public List<UserAchievement> GetUserAchievements(int userId)
        {
            List<UserAchievement> userAchievements = new List<UserAchievement>();

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM UserAchievements WHERE UserId = @UserId", connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userAchievements.Add(new UserAchievement
                            {
                                UserAchievementId = Convert.ToInt32(reader["UserAchievementId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                AchievementId = Convert.ToInt32(reader["AchievementId"]),
                                Earned = Convert.ToBoolean(reader["Earned"])
                            });
                        }
                    }
                }
            }

            return userAchievements;
        }

        public UserAchievement GetUserAchievementByUserIdAndAchievementId(int userId, int achievementId)
        {
            UserAchievement userAchievement = null;

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM UserAchievements WHERE UserId = @UserId AND AchievementId = @AchievementId", connection))
                {
                    cmd.Parameters.AddWithValue("UserId", userId);
                    cmd.Parameters.AddWithValue("AchievementId", achievementId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userAchievement = new UserAchievement
                            {
                                UserAchievementId = Convert.ToInt32(reader["UserAchievementId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                AchievementId = Convert.ToInt32(reader["AchievementId"]),
                                Earned = Convert.ToBoolean(reader["Earned"])
                            };
                        }
                    }
                }
            }
            return userAchievement;
        }

        public void Update(UserAchievement userAchievement)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("UPDATE UserAchievements SET Earned = @Earned WHERE UserAchievementId = @UserAchievementId", connection))
                {
                    cmd.Parameters.AddWithValue("Earned", userAchievement.Earned);
                    cmd.Parameters.AddWithValue("UserAchievementId", userAchievement.UserAchievementId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
