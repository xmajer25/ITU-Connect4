using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.ViewModel.Interfaces
{
    public interface ILoadUser
    {
        void LoadUser(User currentUser);
    }
}
