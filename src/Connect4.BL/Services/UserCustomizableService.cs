using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : UserCustomizableService
 * Brief    : Implements functions for View and data communication
 */


namespace Connect4.BL.Services
{
    public class UserCustomizableService
    {
        private JoinUserCustomRepository _userCustomizableRepository;
        private CustomizableService _customizableService;

        public UserCustomizableService()
        {
            _userCustomizableRepository = new JoinUserCustomRepository();
            _customizableService = new CustomizableService();
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

        public void SaveUserCustom(int UserId, string ImagePath, int Own)
        {
            int id = _customizableService.GetIdByImagePath(ImagePath);
            
            JoinUserCustom model = new JoinUserCustom();
            model.UserId = UserId;
            model.CustomizableId = id;
            model.Ownership = Own;

            _userCustomizableRepository.Create(model);
        }

        public void UpdateOwnership(int UserId, string ImagePath, int Ownership)
        {
            int id = _customizableService.GetIdByImagePath(ImagePath);

            _userCustomizableRepository.UpdateOwnership(UserId, id, Ownership);
        }

        public void SetSelected(int UserId, string ImagePath, int skinType, bool token2)
        {
            string imagePathOld;
            if (!token2) {
                imagePathOld = _customizableService.GetCustomizablesForUser(UserId, 1, skinType)[0];
                // set new as Selected
                UpdateOwnership(UserId, ImagePath, 1);
            }
            else
            {
                imagePathOld = _customizableService.GetCustomizablesForUser(UserId, 2, skinType)[0];
                // set new as Token2
                UpdateOwnership(UserId, ImagePath, 2);
            }
            

            // set old as owmned
            UpdateOwnership(UserId, imagePathOld, 0);

            
        }
    }
}
