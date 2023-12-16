using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.Repositories
{
    public class GridPopRepository
    {
        public GridModelPop GetGridModel()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM GameStates ORDER BY Id DESC LIMIT 1"; // Retrieve the latest game state

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GridModelPop
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Grid = DeserializeGrid(reader["GridData"].ToString()), 
                                CurrentPlayer = Convert.ToInt32(reader["CurrentPlayer"])
                            };
                        }
                    }
                }
            }

            return null; // Return null if the game state is not found
        }

        public void SaveGridModel(GridModelPop model)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string saveQuery = @"
                INSERT INTO GameStates (Id,CurrentPlayer, GridData, Rows, Columns)
                VALUES (@Id, @CurrentPlayer, @GridData, @Rows, @Columns);
            ";

                using (var command = new SQLiteCommand(saveQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", 1);
                    command.Parameters.AddWithValue("@CurrentPlayer", model.CurrentPlayer);
                    command.Parameters.AddWithValue("@GridData", SerializeGrid(model.Grid));
                    command.Parameters.AddWithValue("@Rows", model.Rows);
                    command.Parameters.AddWithValue("@Columns", model.Columns);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateGridModel(int gameId, int[] newGrid, int newCurrentPlayer)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string updateQuery = @"
                UPDATE GameStates
                SET CurrentPlayer = @CurrentPlayer, GridData = @GridData
                WHERE Id = @Id;
            ";

                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", gameId);
                    command.Parameters.AddWithValue("@CurrentPlayer", newCurrentPlayer);
                    command.Parameters.AddWithValue("@GridData", SerializeGrid(newGrid));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteGridModel(int gameId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string deleteQuery = @"
                DELETE FROM GameStates
                WHERE Id = @Id;
            ";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetRowsCount(int gameId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT Rows FROM GameStates WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", gameId);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int rows))
                    {
                        return rows;
                    }
                }
            }

            return 6;
        }

        public int GetColumnsCount(int gameId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT Columns FROM GameStates WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", gameId);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int columns))
                    {
                        return columns;
                    }
                }
            }

            return 8;
        }


        private string SerializeGrid(int[] grid)
        {
            
            return JsonConvert.SerializeObject(grid);
        }

        private int[] DeserializeGrid(string serializedGrid)
        {
            
            return JsonConvert.DeserializeObject<int[]>(serializedGrid);
        }
    }
}
