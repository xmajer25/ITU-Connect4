using System;
using System.Collections.Generic;

namespace Connect4.DAL.DataModels
{
    public class GridModelStandard
    {
        public List<List<CellState>> Grid { get; private set; }
        public int CurrentPlayer { get; private set; }

        private const int Rows = 6;
        private const int Columns = 8;

        public GridModelStandard()
        {
            Grid = new List<List<CellState>>();
            for (int col = 0; col < Columns; col++)
            {
                var column = new List<CellState>();
                for (int row = 0; row < Rows; row++)
                {
                    column.Add(CellState.Empty);
                }
                Grid.Add(column);
            }
            CurrentPlayer = 1;
        }

        // Copy constructor for cloning
        public GridModelStandard(GridModelStandard other)
        {
            Grid = new List<List<CellState>>();
            foreach (var column in other.Grid)
            {
                Grid.Add(new List<CellState>(column));
            }
            CurrentPlayer = other.CurrentPlayer;
        }
    }

    public enum CellState
    {
        Empty,
        Player1,
        Player2
    }
}
