using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

/*
 * Author   : Dušan Slúka (xsluka00)
 * File     : GridStandardService
 * Brief    : Game logic for standard mode of connect 4
 */

namespace Connect4.BL.Services
{
    // A service class responsible for managing application settings.
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
