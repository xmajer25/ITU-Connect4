using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

namespace Connect4.BL.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public List<string> GetAllUsernames()
        {

            return _userRepository.GetAllUsernames();
        }

        public bool IsUserNameRegistered(string username)
        {
            return _userRepository.GetAllUsernames().Contains(username);
        }

        public bool IsUserEmailRegistered(string email)
        {
            return _userRepository.GetAllEmails().Contains(email);
        }

        public void CreateUser(string name, string password, string email) 
        {
            User newUser = new User();
            newUser.Username = name;
            newUser.Password = password;
            newUser.Email = email;
            newUser.GamesPlayed = 0;
            newUser.GamesWon = 0;
            newUser.GoldTotal = 0;
            newUser.GoldActual = 0;
            _userRepository.CreateUser(newUser);
        }

        public User UpdateUser(int userId, string username, string password, string email, int gamesPlayed, int gamesWon, int goldTotal, int goldActual)
        {
            User updatedUser = new User
            {
                Id = userId,
                Username = username,
                Password = password,  // Hashed and salted before storage
                Email = email,
                GamesPlayed = gamesPlayed,
                GamesWon = gamesWon,
                GoldTotal = goldTotal,
                GoldActual = goldActual
            };

            _userRepository.UpdateUser(updatedUser);
            return updatedUser;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public int UpdateGold(int userId, int goldDeducted) {
            int goldBefore = _userRepository.GetGoldActual(userId);

            _userRepository.UpdateGoldActual(userId, goldBefore-goldDeducted);

            return goldBefore - goldDeducted;

        }
    }
}
