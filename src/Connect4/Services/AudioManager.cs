using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Connect4.Services
{
    public class AudioManager
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static Uri soundUri = new Uri("pack://siteoforigin:,,,/Resources/Sounds/DefaultSound.mp3", UriKind.RelativeOrAbsolute);

        public static void PlaySound()
        {
            if (mediaPlayer.Source == null)
            {
                mediaPlayer.Open(soundUri);
                mediaPlayer.MediaEnded += (sender, e) => mediaPlayer.Position = TimeSpan.Zero;
            }
            
            mediaPlayer.Play();
        }
    }
}
