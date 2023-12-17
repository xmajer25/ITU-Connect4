using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : JoinUserCustom
 * Brief    : Data model for join table of users and customizables
 */


namespace Connect4.DAL.DataModels
{
    public class JoinUserCustom
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomizableId { get; set; }
        public int Ownership { get; set; }
        /* 0 - own
         * 1 - selected / token 1
         * 2 - token 2
         */
    }
}
