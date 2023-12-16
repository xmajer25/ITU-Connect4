using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Connect4.Services
{
    public class AudioManager
    {
        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

        private MediaElement mediaElement;

        private AudioManager()
        {
            mediaElement = new MediaElement();
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Stop;
            mediaElement.MediaEnded += MediaElement_MediaEnded;

            // Set the default audio source
            mediaElement.Source = new Uri("pack://siteoforigin:,,,/Resources/Sounds/DefaultSound.mp3");
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.Zero;
            mediaElement.Play();
        }

        public MediaElement GetMediaElement()
        {
            return mediaElement;
        }
    }
}
