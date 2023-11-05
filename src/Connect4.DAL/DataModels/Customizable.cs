using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.DataModels
{
    public class Customizable
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public bool IsToken {  get; set; }
        public bool IsAvatar { get; set; }
        public bool IsBack {  get; set; }
    }
}
