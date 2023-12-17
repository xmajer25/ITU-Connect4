using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut(xmahut01)
 * File     : User
 * Brief    : Data model to store User information
 */

namespace Connect4.DAL.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // this is hashed and salted before storage
        public string Email { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon {  get; set; }
        public int GoldTotal { get; set; }
        public int GoldActual {  get; set; }

    }
}
