using System;
using System.Collections.Generic;

namespace Connect4.DAL.DataModels
{
    /*
    * Author   : Dušan Slúka
    * File     : GridModelStandard
    * Brief    : Represents the data model for the game grid in Connect4. 
    *            This model maintains a two-dimensional list representing the grid cells' states,
    *            where each cell can be empty, occupied by Player1, or Player2.
    *            It also tracks the current player for the game. 
    */

    public class GridModelStandard
    {
        // A two-dimensional list representing the grid cells' states
        public List<List<CellState>> Grid { get; private set; }
        // Tracks which player's turn it is (1 or 2)
        public int CurrentPlayer { get; private set; }

        // Constants for the number of rows and columns in the grid
        private const int Rows = 6;
        private const int Columns = 8;

        // Constructor initializes the grid with empty cells and sets the starting player
        public GridModelStandard()
        {
            Grid = new List<List<CellState>>();
            for (int col = 0; col < Columns; col++)
            {
                var column = new List<CellState>();
                for (int row = 0; row < Rows; row++)
                {
                    column.Add(CellState.Empty); // Each cell in the column is initially empty
                }
                Grid.Add(column); // Add the column to the grid
            }
            CurrentPlayer = 1; // Start with Player 1
        }

        // Copy constructor to create a deep copy of the grid model
        public GridModelStandard(GridModelStandard other)
        {
            Grid = new List<List<CellState>>();
            foreach (var column in other.Grid)
            {
                Grid.Add(new List<CellState>(column)); // Copy each column
            }
            CurrentPlayer = other.CurrentPlayer; // Copy the current player
        }
    }

    // Enum to represent the state of each cell in the grid
    public enum CellState
    {
        Empty,    // Cell is empty
        Player1,  // Cell is occupied by Player 1
        Player2   // Cell is occupied by Player 2
    }

}
