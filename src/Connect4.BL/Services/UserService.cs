using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
