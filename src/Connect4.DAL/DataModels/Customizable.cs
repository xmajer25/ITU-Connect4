using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut (xmahut01) 
 * File     : Customizables
 * Brief    : Data model to store information about Customizables
 */

namespace Connect4.DAL.DataModels
{
    public class Customizable
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int Type { get; set; }
        /*
         0 - Bckg
         1 - Tok
         2 - Ava
         */

    }
}
