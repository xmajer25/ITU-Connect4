using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;

namespace Connect4.BL.Services.GridServices
{
    public class GridPopService
    {
        private readonly GridPopRepository _gridPopRepository;

        public GridPopService()
        {
            _gridPopRepository = new GridPopRepository();
        }

        public void StartNewGame()
        {
            // Create a new game state and save it to the database
            var newGridModel = new GridModelPop();
            _gridPopRepository.SaveGridModel(newGridModel);
        }

        public GridModelPop GetCurrentGameState()
        {
            // Retrieve the current game state from the database
            return _gridPopRepository.GetGridModel();
        }


        public int MakePut(int column)
        {
            // Get the current game state
            var currentGridModel = _gridPopRepository.GetGridModel();

            // Check if the move is valid
            int placedRow = FindAvailableRow(currentGridModel.Grid, column);
            if (placedRow != -1)
            {
                // Apply the move to the grid
                ApplyPut(currentGridModel.Grid, column, currentGridModel.CurrentPlayer, placedRow);

                // Switch to the next player and update the game state
                currentGridModel.CurrentPlayer = 3 - currentGridModel.CurrentPlayer; // Toggle between players 1 and 2
                _gridPopRepository.UpdateGridModel(currentGridModel.Id, currentGridModel.Grid, currentGridModel.CurrentPlayer);


                // Return the row where the move was made
                return placedRow;
            }

            // If the move is not valid, return a value indicating that
            return -1; // Or any other suitable indicator for an invalid move
        }

        private int FindAvailableRow(int[] grid, int column)
        {
            int rows = _gridPopRepository.GetRowsCount(1);

            // Find the first available row in the specified column
            for (int row = rows - 1; row >= 0; row--)
            {
                int index = row * _gridPopRepository.GetColumnsCount(1) + column;
                if (grid[index] == 0)
                {
                    // The cell is empty, so the move is valid
                    return row;
                }
            }

            // If the loop completes, the column is full, and the move is not valid
            return -1;
        }

        private void ApplyPut(int[] grid, int column, int currentPlayer, int row)
        {
            int rows = _gridPopRepository.GetRowsCount(1);

            // Apply the move to the grid

            int index = row * _gridPopRepository.GetColumnsCount(1) + column;
            grid[index] = currentPlayer;

        }

        public void MakePop(int column)
        {
            // Get the current game state
            var currentGridModel = _gridPopRepository.GetGridModel();

            // Check if the move is valid
            if (IsValidPop(currentGridModel.Grid, column, currentGridModel.CurrentPlayer))
            {
                // Apply the "pop" move to the grid
                ApplyPop(currentGridModel.Grid, column, currentGridModel.CurrentPlayer);


                // Switch to the next player and update the game state
                currentGridModel.CurrentPlayer = 3 - currentGridModel.CurrentPlayer; // Toggle between players 1 and 2
                _gridPopRepository.UpdateGridModel(currentGridModel.Id, currentGridModel.Grid, currentGridModel.CurrentPlayer);

            }
        }

        private bool IsValidPop(int[] grid, int column, int currentPlayer)
        {
            int rows = _gridPopRepository.GetRowsCount(1);

            // Check if the bottom cell in the specified column belongs to the opposite player
            return grid[(rows - 1) * _gridPopRepository.GetColumnsCount(1) + column] == 3 - currentPlayer;
        }

        private void ApplyPop(int[] grid, int column, int currentPlayer)
        {
            int rows = _gridPopRepository.GetRowsCount(1);
            int columns = _gridPopRepository.GetColumnsCount(1);

            // Find the first non-empty cell in the specified column
            for (int row = 0; row < rows; row++)
            {
                int index = row * columns + column;

                // Check if the cell belongs to the opposite player
                if (grid[index] != currentPlayer)
                {
                    // Remove the token in the cell
                    grid[index] = 0;
                    break;
                }
            }

            // Shift all tokens above down by one position
            for (int row = rows - 1; row > 0; row--)
            {
                int currentIndex = row * columns + column;
                int aboveIndex = (row - 1) * columns + column;

                // Move the token from the cell above to the current cell
                grid[currentIndex] = grid[aboveIndex];
            }

            // Set the top cell to 0
            grid[column] = 0;
        }

        public bool IsWinner()
        {
            var currentGridModel = _gridPopRepository.GetGridModel();

            int rows = _gridPopRepository.GetRowsCount(1);
            int columns = _gridPopRepository.GetColumnsCount(1);

            // Check if board is full
            if (IsGridFull(currentGridModel.Grid)) return true;

            // Check horizontally
            if (CheckDirection(currentGridModel.Grid, rows, columns, currentGridModel.CurrentPlayer, 0, 1)) return true;

            // Check vertically
            if (CheckDirection(currentGridModel.Grid, rows, columns, currentGridModel.CurrentPlayer, 1, 0)) return true;

            // Check diagonally (bottom-left to top-right)
            if (CheckDirection(currentGridModel.Grid, rows, columns, currentGridModel.CurrentPlayer, -1, 1)) return true;

            // Check diagonally (top-left to bottom-right)
            if (CheckDirection(currentGridModel.Grid, rows, columns, currentGridModel.CurrentPlayer, 1, 1)) return true;

            // No winner found
            return false;
        }

        private bool CheckDirection(int[] grid, int rows, int columns, int currentPlayer, int rowDirection, int colDirection)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    int index = row * columns + col;

                    // Check if the current cell belongs to the current player
                    if (grid[index] == currentPlayer)
                    {
                        // Check for a sequence of four in the specified direction
                        int count = CountSequence(grid, rows, columns, currentPlayer, row, col, rowDirection, colDirection);

                        if (count == 4)
                        {
                            return true; // Found a winner
                        }
                    }
                }
            }

            return false;
        }
        private int CountSequence(int[] grid, int rows, int columns, int currentPlayer, int startRow, int startCol, int rowDirection, int colDirection)
        {
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                int row = startRow + i * rowDirection;
                int col = startCol + i * colDirection;

                // Check if the cell is within bounds
                if (row >= 0 && row < rows && col >= 0 && col < columns)
                {
                    int index = row * columns + col;

                    // Check if the cell belongs to the current player
                    if (grid[index] == currentPlayer)
                    {
                        count++;
                    }
                    else
                    {
                        break; // Break the sequence if a different player's token is encountered
                    }
                }
                else
                {
                    break; // Break the sequence if the cell is out of bounds
                }
            }

            return count;
        }


        private bool IsGridFull(int[] grid)
        {
            int rows = _gridPopRepository.GetRowsCount(1);
            int columns = _gridPopRepository.GetColumnsCount(1);

            for (int i = 0; i < rows * columns; i++)
            {
                if (grid[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void SwapPlayers()
        {
            var currentGridModel = _gridPopRepository.GetGridModel();

            // Swap players (assuming 1 and 2 are the possible player values)
            currentGridModel.CurrentPlayer = 3 - currentGridModel.CurrentPlayer;

            // Save the updated model back to the database
            _gridPopRepository.UpdateGridModel(currentGridModel.Id, currentGridModel.Grid, currentGridModel.CurrentPlayer);
        }

        public int GetCurrentPlayer()
        {
            var currentGridModel = _gridPopRepository.GetGridModel();
            return currentGridModel.CurrentPlayer;
        }

        public void DeleteGame(int gameId)
        {
            _gridPopRepository.DeleteGridModel(gameId);
        }
    }
}
