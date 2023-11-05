using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

namespace Connect4.BL.Services
{
    public class UserCustomizableService
    {
        private JoinUserCustomRepository _userCustomizableRepository;

        public UserCustomizableService(JoinUserCustomRepository userCustomizableRepository)
        {
            _userCustomizableRepository = userCustomizableRepository;
        }

        // Retrieve UserCustomizable records by IsToken flag and UserId
        public List<JoinUserCustom> GetUserCustomizablesByIsTokenAndUser(int userId)
        {
            return _userCustomizableRepository.GetUserCustomizablesByIsTokenAndUser(userId);
        }

        // Retrieve UserCustomizable records by IsAvatar flag and UserId
        public List<JoinUserCustom> GetUserCustomizablesByIsAvatarAndUser(int userId)
        {
            return _userCustomizableRepository.GetUserCustomizablesByIsAvatarAndUser(userId);
        }

        // Retrieve UserCustomizable records by IsBack flag and UserId
        public List<JoinUserCustom> GetUserCustomizablesByIsBackAndUser(int userId)
        {
            return _userCustomizableRepository.GetUserCustomizablesByIsBackAndUser(userId);
        }

    }
}
