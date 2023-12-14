using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BL.Services
{
    public class UserAchievementService
    {
        private JoinUserAchievRepository _joinUARepository;

        public UserAchievementService()
        {
            _joinUARepository = new JoinUserAchievRepository();
        }

        public List<UserAchievement> GetUserAchievements(int userId)
        {
            List<UserAchievement> userAchievements = _joinUARepository.GetUserAchievements(userId);
            return userAchievements;
        }

        public UserAchievement UpdateUserAchievement(int userId, int achievementId, bool earned)
        {
            // Check if the user achievement exists
            UserAchievement userAchievement = _joinUARepository.GetUserAchievementByUserIdAndAchievementId(userId, achievementId);

            if (userAchievement != null)
            {
                userAchievement.Earned = earned;
                _joinUARepository.Update(userAchievement);
            }

            return userAchievement;
        }

    }
}
