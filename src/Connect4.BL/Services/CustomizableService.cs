using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

namespace Connect4.BL.Services
{
    public class CustomizableService
    {
        private CustomizableRepository _customizableRepository;

        public CustomizableService(CustomizableRepository customizableRepository)
        {
            _customizableRepository = customizableRepository;
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
