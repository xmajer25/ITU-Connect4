﻿using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Connect4.DAL.DatabaseHelpers
{
    public class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Create the table if it doesn't exist
                string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE
            );
            
            CREATE TABLE IF NOT EXISTS Achievements(
                AchievementId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT
            );
                
            CREATE TABLE IF NOT EXISTS UserAchievements(
                UserAchievementId INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER,
                AchievementId INTEGER,
                Earned BOOLEAN
            );
                ";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert dummy values if no users exist yet
                UserRepository userRepository = new UserRepository();
                if (!userRepository.AnyUsersExist())
                {
                    InsertDummyUsers(connection);
                }
            }
        }

        private static void InsertDummyUsers(SQLiteConnection connection)
        {
            string insertDummyQuery = @"
        INSERT INTO Users (Username, Password, Email) VALUES 
        ('DummyUser1', 'DummyPass1', 'dummy1@email.com'),
        ('DummyUser2', 'DummyPass2', 'dummy2@email.com'),
        ('DummyUser3', 'DummyPass3', 'dummy3@email.com');

        INSERT INTO UserAchievements (Name, Description, Earned) VALUES
        ('Getting Started!', 'Play one game of Connect4.'),
        ('Played 10!','Play ten games of Connect4.'),
        ('Played 100!','Play hundred games of Connect4.'),
        ('Played 1000!','Play thousand games of Connect4.'),
        ('THE LEGEND!!!','Play ten thousand games of Connect4.'),
        ('4-in-a-Row!','Win in first 4 turns.'),
        ('No rookie anymore!','Win fifty games of Connect4!'),
        ('Connect4 Veteran!','Win five hundred games of Connect4.'),
        ('Every token counts!','Win by placing the last token of the game.'),
        ('The Ultimate Collector!','Collect all cosmetics.');

        INSERT INTO UserAchievements (UserAchievementId, UserId, AchievementId, Earned) VALUES
         (1, 1, 1, 1),
         (2, 1, 2, 0),
         (3, 2, 1, 1),
         (4, 2, 2, 1),
         (5, 2, 3, 0);
        ";

            using (var command = new SQLiteCommand(insertDummyQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
