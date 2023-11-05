﻿using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BL.Services
{
    public class AchievementService
    {
        private AchievementRepository _achievRepository;

        public AchievementService()
        {
            _achievRepository = new AchievementRepository();
        }

        public Achievement GetAchievementById(int achievementId)
        {
            Achievement achievement = _achievRepository.GetAchievementById(achievementId);
            return achievement;
        }
    }
}
