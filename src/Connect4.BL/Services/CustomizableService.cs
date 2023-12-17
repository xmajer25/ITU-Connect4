using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : CustomizableService
 * Brief    : Implements functions for View and data communication of Customizables data model
 */


namespace Connect4.BL.Services
{
    public class CustomizableService
    {
        private CustomizableRepository _customizableRepository;

        public CustomizableService()
        {
            _customizableRepository = new CustomizableRepository();
        }

        public ObservableCollection<string> GetCustomizablesForUser(int userId, int Own, int Type)
        {
            return _customizableRepository.GetCustomizablesForUser(userId, Own, Type);
        }

        public ObservableCollection<string> GetAvailableCustomizables(int UserId, int Type)
        {
            return _customizableRepository.GetAvailableCustomizables(UserId, Type);
        }

        public int GetIdByImagePath(string imagePath)
        {
            return _customizableRepository.GetIdByImagePath(imagePath);
        }
    }
}
