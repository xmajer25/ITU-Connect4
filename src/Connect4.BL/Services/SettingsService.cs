using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BL.Services
{
    public class SettingsService
    {
       
        private readonly SettingsRepository _settingsRepository;
        private readonly int _currentUserId;

        public SettingsService(int currentUserId)
        {
            _settingsRepository = new SettingsRepository();
            _currentUserId = currentUserId;
        }


        public Settings LoadSettings()
        {
            var settings = _settingsRepository.GetSettingsByUserId(_currentUserId);
            if (settings == null)
            {
                settings = new Settings
                {
                    UserId = _currentUserId,
                    IsNarrationEnabled = false, // Set default values
                    // Add other default settings here
                };
                _settingsRepository.Create(settings);
            }
            return settings;
        }

        public void SaveSettings(Settings settings)
        {
            settings.UserId = _currentUserId;
            _settingsRepository.Update(settings);
        }
    }


}
