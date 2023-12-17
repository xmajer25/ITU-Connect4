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
    public class SettingsRepository
    {
        public void Create(Settings settings)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            INSERT INTO Settings (UserId, MasterVolume, EffectVolume, VoiceNaration) 
            VALUES (@UserId, @MasterVolume, @EffectVolume, @VoiceNarration)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", settings.UserId);
                    cmd.Parameters.AddWithValue("@MasterVolume", settings.MasterVolume);
                    cmd.Parameters.AddWithValue("@EffectVolume", settings.EffectVolume);
                    cmd.Parameters.AddWithValue("@VoiceNarration", settings.IsNarrationEnabled ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update an existing Settings record
        public void Update(Settings settings)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            UPDATE Settings 
            SET MasterVolume = @MasterVolume, 
                EffectVolume = @EffectVolume, 
                VoiceNaration = @VoiceNarration
            WHERE UserId = @UserId";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", settings.UserId);
                    cmd.Parameters.AddWithValue("@MasterVolume", settings.MasterVolume);
                    cmd.Parameters.AddWithValue("@EffectVolume", settings.EffectVolume);
                    cmd.Parameters.AddWithValue("@VoiceNarration", settings.IsNarrationEnabled ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Settings GetSettingsByUserId(int userId)
        {
            Settings settings = null;

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Settings WHERE UserId = @UserId";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            settings = new Settings
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                MasterVolume = Convert.ToInt32(reader["MasterVolume"]),
                                EffectVolume = Convert.ToInt32(reader["EffectVolume"]),
                                IsNarrationEnabled = Convert.ToInt32(reader["VoiceNaration"]) == 1 // Assuming 1 for true, 0 for false
                            };
                        }
                    }
                }
            }

            return settings;
        }
    }
}
