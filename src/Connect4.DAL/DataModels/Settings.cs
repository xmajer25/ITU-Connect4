using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.DataModels
{
    public class Settings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MasterVolume { get; set; }
        public int EffectVolume { get; set; }
        public bool IsNarrationEnabled { get; set; }
    }
}
