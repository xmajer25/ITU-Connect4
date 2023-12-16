using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories.Interfaces;
using System.Linq;
/*
 * Author   : Dušan Slúka (xsluka00)
 * File     : GridStandardService
 * Brief    : Game logic for standard mode of connect 4
 */

namespace Connect4.BL.Services
{
    public class GridStandardService
    {
        private readonly IGridStandardRepository _repository;

        public GridStandardService(IGridStandardRepository repository)
        {
            _repository = repository;
        }

        public GridModelStandard GetGridModel()
        {
            // Fetch and return the current grid model from the repository
            return _repository.GetGridModel();
        }

        // Example method to make a move
        public bool MakeMove(int column, CellState player)
        {
            var gridModel = _repository.GetGridModel();

            // Find the lowest empty row in the column
            int emptyRow = GetMaxRow(column);
            if (emptyRow == -1) return false; // Column is full, so the move cannot be made

            // Place the player's piece in the found empty row
            gridModel.Grid[column][emptyRow] = player;

            // Update the grid model in the repository
            _repository.UpdateGridModel(gridModel);

            // Optionally, check for win condition after the move
            //return CheckWinCondition(gridModel);
            return true;
        }


        public bool IsBoardFull(GridModelStandard gridModel)
        {
            // Iterate through each column in the grid.
            // If any column is not full, return false.
            // If all columns are full, return true.
            for (int col = 0; col < gridModel.Grid.Count; col++)
            {
                if (!IsColumnFull(col, gridModel))
                {
                    // Found a column that is not full, hence the board is not full.
                    return false;
                }
            }

            // All columns are full, so the board is full.
            return true;
        }

        public int GetMaxRow(int column)
        {
            var gridModel = _repository.GetGridModel();

            // If the column is full, return -1 immediately
            if (IsColumnFull(column, gridModel))
            {
                return -1;
            }

            var columnData = gridModel.Grid[column];

            // Find the lowest empty row in the column
            for (int row = columnData.Count - 1; row >= 0; row--)
            {
                if (columnData[row] == CellState.Empty)
                {
                    return row;
                }
            }

            // This line should theoretically never be reached because IsColumnFull would have caught it
            return -1;
        }

        public int GetFirstEmpty(int column)
        {
            var gridModel = _repository.GetGridModel();

            // If the column is full, return -1 immediately
            if (IsColumnFull(column, gridModel))
            {
                return -1;
            }

            var columnData = gridModel.Grid[column];

            // Find the lowest empty row in the column
            for (int row = 0; row <= columnData.Count - 1; row++)
            {
                if (columnData[row] == CellState.Empty)
                {
                    return row;
                }
            }

            // This line should theoretically never be reached because IsColumnFull would have caught it
            return -1;
        }

        private bool IsColumnFull(int column, GridModelStandard gridModel)
        {
            // Check if the column is full
            
            return gridModel.Grid[column].All(cell => cell != CellState.Empty);
        }

        public bool CheckWinCondition(GridModelStandard gridModel)
        {
            return CheckHorizontalWin(gridModel) ||
                   CheckVerticalWin(gridModel) ||
                   CheckDiagonalDownRightWin(gridModel) ||
                   CheckDiagonalDownLeftWin(gridModel);
        }

        private bool CheckHorizontalWin(GridModelStandard gridModel)
        {
            int rows = gridModel.Grid[0].Count;
            int columns = gridModel.Grid.Count;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col <= columns - 4; col++)
                {
                    if (gridModel.Grid[col][row] != CellState.Empty &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 1][row] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 2][row] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 3][row])
                        return true;
                }
            }
            return false;
        }

        private bool CheckVerticalWin(GridModelStandard gridModel)
        {
            int rows = gridModel.Grid[0].Count;
            int columns = gridModel.Grid.Count;

            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row <= rows - 4; row++)
                {
                    if (gridModel.Grid[col][row] != CellState.Empty &&
                        gridModel.Grid[col][row] == gridModel.Grid[col][row + 1] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col][row + 2] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col][row + 3])
                        return true;
                }
            }
            return false;
        }


        private bool CheckDiagonalDownRightWin(GridModelStandard gridModel)
        {
            int rows = gridModel.Grid[0].Count;
            int columns = gridModel.Grid.Count;

            for (int col = 0; col <= columns - 4; col++)
            {
                for (int row = 0; row <= rows - 4; row++)
                {
                    if (gridModel.Grid[col][row] != CellState.Empty &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 1][row + 1] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 2][row + 2] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 3][row + 3])
                        return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalDownLeftWin(GridModelStandard gridModel)
        {
            int rows = gridModel.Grid[0].Count;
            int columns = gridModel.Grid.Count;

            for (int col = 0; col <= columns - 4; col++)
            {
                for (int row = 3; row < rows; row++)
                {
                    if (gridModel.Grid[col][row] != CellState.Empty &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 1][row - 1] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 2][row - 2] &&
                        gridModel.Grid[col][row] == gridModel.Grid[col + 3][row - 3])
                        return true;
                }
            }
            return false; 
        }
    }
}
