using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BL.Services.GridServices
{
    public class GridCrazyHouseService : GridBaseService
    {
        public GridCrazyHouseService() : base()
        {
        }

        public bool IsColumnFull(int column)
        {
            int[,] grid = GetGrid();

            for(int row = 0; row < GetGridRows(); row++) 
                if (grid[row, column] == 0) return false;

            return true;
        }

        public bool IsGridFull()
            => GetEmptyColumns().Count == 0;
        

        public List<int> GetEmptyColumns()
        {
            List<int> emptyColumns = new List<int>();

            for(int column = 0; column < GetGridColumns(); column++)
            {
                if(!IsColumnFull(column)) emptyColumns.Add(column);
            }

            return emptyColumns;
        }

        public (List<bool>, int endingColumn) GetDropPath(int startingColumn)
        {
            Random random = new Random();
            List<int> possibleColumns = GetEmptyColumns();
            List<bool> path = new List<bool>();
            int maxWidth = GetGridColumns(); int minWidth = 0;
            int endingColumn;

            int randomColumnIndex = random.Next(0, possibleColumns.Count);
            endingColumn = possibleColumns[randomColumnIndex];

            while (path.Count < 7)
            {
                bool goRight;
                if (startingColumn - 1 < minWidth) goRight = true;
                else if (startingColumn + 1 > maxWidth) goRight = false;
                else goRight = startingColumn < endingColumn || (startingColumn == endingColumn && random.Next(2) == 0);

                path.Add(goRight);
                startingColumn += goRight ? 1 : -1;
            }

            return (path, endingColumn);
        }

        public int GetMaxRow(int column)
        {
            int[,] grid = GetGrid();

            var emptyRow = Enumerable.Range(0, GetGridRows())
            .Reverse()
            .Where(row => grid[row, column] == 0)
            .First();

            return emptyRow;
        }

        public List<bool> PlayMove(int player, int startingColumn)
        {
            List<bool> path;
            int endingColumn ;
            int changedRow;

            (path, endingColumn) = GetDropPath(startingColumn);

            changedRow = GetMaxRow(endingColumn);

            SetCell(changedRow, endingColumn, player);

            return path;
        }

        public bool CheckWinHorizontal(int player)
        {
            int[,] grid = GetGrid();

            for (int row = 0; row < GetGridRows(); row++)
            {
                for (int col = 0; col <= GetGridColumns() - 4; col++)
                {
                    if (grid[row, col] == player &&
                        grid[row, col + 1] == player &&
                        grid[row, col + 2] == player &&
                        grid[row, col + 3] == player)
                    {
                        return true; // Horizontal win
                    }
                }
            }

            return false;
        }

        public bool CheckWinVertical(int player)
        {
            int[,] grid = GetGrid();

            for (int row = 0; row <= GetGridRows() - 4; row++)
            {
                for (int col = 0; col < GetGridColumns(); col++)
                {
                    if (grid[row, col] == player &&
                        grid[row + 1, col] == player &&
                        grid[row + 2, col] == player &&
                        grid[row + 3, col] == player)
                    {
                        return true; // Vertical win
                    }
                }
            }

            return false;
        }

        public bool CheckWinDiagonal(int player)
        {
            int[,] grid = GetGrid();

            for (int row = 0; row <= GetGridRows() - 4; row++)
            {
                for (int col = 0; col <= GetGridColumns() - 4; col++)
                {
                    if (grid[row, col] == player &&
                        grid[row + 1, col + 1] == player &&
                        grid[row + 2, col + 2] == player &&
                        grid[row + 3, col + 3] == player)
                    {
                        return true;
                    }
                }
            }

            for (int row = 0; row <= GetGridRows() - 4; row++)
            {
                for (int col = GetGridColumns() - 1; col >= 3; col--)
                {
                    if (grid[row, col] == player &&
                        grid[row + 1, col - 1] == player &&
                        grid[row + 2, col - 2] == player &&
                        grid[row + 3, col - 3] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckForWin(int player)
        {
            if(CheckWinHorizontal(player)) return true;
            if(CheckWinVertical(player)) return true;
            if(CheckWinDiagonal(player)) return true;

            return false;  
        }
    }
}
