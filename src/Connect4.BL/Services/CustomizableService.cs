using System;
using System.Collections.Generic;
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

        public List<Customizable> GetAllCustomizables()
        {
            List<Customizable> customizables = _customizableRepository.GetAllCustomizables();
            return customizables;
        }

        public Customizable GetCustomizableById(int id)
        {
            return _customizableRepository.GetCustomizableById(id);
        }
    }
}
