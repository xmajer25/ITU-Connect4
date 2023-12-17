using Connect4.DAL.DataModels;
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
            CREATE TABLE IF NOT EXISTS Users(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE,
                GamesPlayed INTEGER NOT NULL,
                GamesWon INTEGER NOT NULL,
                GoldTotal INTEGER NOT NULL,
                GoldActual INTEGER NOT NULL
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

            CREATE TABLE IF NOT EXISTS Settings (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER NOT NULL,
                MasterVolume INTEGER NOT NULL,
                VoiceNaration INTEGER NOT NULL,
                EffectVolume INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Customizables (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ImagePath TEXT NOT NULL,
                Price INTEGER NOT NULL,
                Type INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS UserCustomizables (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER NOT NULL,
                CustomizableId INTEGER NOT NULL,
                Ownership INTEGER NOT NULL
            );
            
            CREATE TABLE IF NOT EXISTS GameStates (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Rows INTEGER NOT NULL,
                Columns INTEGER NOT NULL,
                CurrentPlayer INTEGER NOT NULL,
                GridData TEXT NOT NULL
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
        INSERT INTO Users (Id, Username, Password, Email, GamesPlayed, GamesWon, GoldTotal, GoldActual) VALUES 
        (1, 'DummyUser1', 'DummyPass1', 'dummy1@email.com', 0, 0, 0, 0),
        (2, 'DummyUser2', 'DummyPass2', 'dummy2@email.com', 0, 0, 0, 0),
        (4, 'Rudo', 'Pass', 'dummy3@email.com', 0, 0, 0, 5000);

        INSERT INTO Achievements (Name, Description) VALUES
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

        INSERT INTO Customizables (Id, ImagePath, Type) VALUES
        (1,'/Resources/Images/BackGrounds/BackGroundDefault.png', 0),
        (2,'/Resources/Images/BackGrounds/BackGroundForest.png', 0),
        (3,'/Resources/Images/Avatars/AvatarBlue.png', 2),
        (4,'/Resources/Images/Avatars/AvatarGreen.png', 2),
        (5,'/Resources/Images/Avatars/AvatarRn.png',2),
        (6,'/Resources/Images/Tokens/TokenBlue.png',1),
        (7,'/Resources/Images/Tokens/TokenGreen.png',1),
        (8,'/Resources/Images/Tokens/TokenGrey.png',1),
        (9,'/Resources/Images/Tokens/TokenOrange.png',1),
        (10,'/Resources/Images/Tokens/TokenPink.png',1),
        (11,'/Resources/Images/Tokens/TokenPurple.png',1),
        (12,'/Resources/Images/Tokens/TokenRed.png',1);
        

        INSERT INTO JoinUserCustom (UserId, CustomizableId, Ownership) VALUES
        (4, 1, 1),
        (4, 5, 1),
        (4, 6, 1),
        (4, 12, 2),
        (1, 1, 1),
        (1, 5, 1),
        (1, 6, 1),
        (1, 12, 2),
        (2, 1, 1),
        (2, 5, 1),
        (2, 6, 1),
        (2, 12, 2);
        ";

        

            using (var command = new SQLiteCommand(insertDummyQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
