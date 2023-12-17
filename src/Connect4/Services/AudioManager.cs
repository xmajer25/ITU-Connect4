using System;
using System.Windows.Media;

namespace Connect4.Services
{
    public class AudioManager
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static Uri soundUri = new Uri("pack://siteoforigin:,,,/Resources/Sounds/DefaultSound.mp3", UriKind.RelativeOrAbsolute);
        private static double volume = 1.0; // Volume in range 0.0 (mute) to 1.0 (max)

        // Method to play sound
        public static void PlaySound()
        {
           

            if (mediaPlayer.Source == null)
            {
                mediaPlayer.Open(soundUri);
                mediaPlayer.MediaEnded += (sender, e) => mediaPlayer.Position = TimeSpan.Zero;
            }

            mediaPlayer.Volume = volume;
            mediaPlayer.Play();
        }

        // Method to set the sound volume
        public static void SetVolume(double newVolume)
        {
            volume = newVolume;
        }
    }
}
