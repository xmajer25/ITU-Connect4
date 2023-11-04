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
            _userRepository.CreateUser(newUser);
        }
    }
}
