using Connect4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public class AudioFeedbackService : IAudioFeedbackService
    {
        private SpeechSynthesizer synthesizer;

        public AudioFeedbackService()
        {
            synthesizer = new SpeechSynthesizer();
            
        }

        public void AnnounceTokenPlacement(int column, int row)
        {
            string announcement = $"Token placed at column {column}, {row} tokens high.";
            synthesizer.SpeakAsync(announcement);
        }

        public void AnnouncePlayerChange(string playerTurn)
        {
            string announcement = $"It is now {playerTurn}'s turn.";
            synthesizer.SpeakAsync(announcement);
        }
    }
}
