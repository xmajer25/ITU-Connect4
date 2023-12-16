using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Services.Interfaces
{
    public interface IAudioFeedbackService
    {
        void AnnounceTokenPlacement(int column, int row);
        void AnnouncePlayerChange(string playerTurn);
    }
}
