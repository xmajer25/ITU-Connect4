using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : JoinUserAchiev
 * Brief    : Data model for join table of users and achievements
 */

namespace Connect4.DAL.DataModels
{
    public class UserAchievement
    {
        public int UserAchievementId { get; set; }
        public int UserId { get; set; }
        public int AchievementId { get; set; }
        public bool Earned { get; set; }
    }
}
