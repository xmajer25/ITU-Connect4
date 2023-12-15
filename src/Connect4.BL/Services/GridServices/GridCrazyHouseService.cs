using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : GridCrazyHouseService
 * Brief    : Specific functions for crazy house mode
 */

namespace Connect4.BL.Services.GridServices
{
    public class GridCrazyHouseService : GridBaseService
    {
        public GridCrazyHouseService() : base()
        {
        }

        /* returns true if column has been filled with tokens */
        public bool IsColumnFull(int column)
        {
            for(int row = 0; row < GetGridRows(); row++) 
                if (GridData.GetValue(row, column) == 0) return false;

            return true;
        }

        /* returns true if there is no more space to place tokens in grid*/
        public bool IsGridFull()
            => GetEmptyColumns().Count == 0;
        
        /* returns list of columns where token can be placed */
        public List<int> GetEmptyColumns()
        {
            List<int> emptyColumns = new List<int>();

            for(int column = 0; column < GetGridColumns(); column++)
            {
                if(!IsColumnFull(column)) emptyColumns.Add(column);
            }

            return emptyColumns;
        }

        /* creates random path for ball drop. */
        /* returns list<bool> where TRUE->go right and FALSE->go left */
        public (List<bool>, int endingColumn) GetDropPath(int startingColumn)
        {
            Random random = new Random();
            List<int> possibleColumns = GetEmptyColumns();
            List<bool> path = new List<bool>();
 
            int endingColumn;

            int randomColumnIndex = random.Next(0, possibleColumns.Count);
            endingColumn = possibleColumns[randomColumnIndex] + 1;

            int steps = endingColumn - startingColumn;
            int totalSteps = Math.Abs(steps) % 2 == 0 ? 7 : 6;

            for (int i = 0; i < totalSteps; i++)
            {
                int step = steps >= 0 ? 1 : -1;
                path.Add(step == 1 ? true : false);
                startingColumn += step;
                steps -= step;
            }

            return (path, endingColumn - 1);
        }

        /* returns position where token should drop -> highest free position in column */
        public int GetMaxRow(int column)
        {
            int rows = GetGridRows(); // Assuming GetGridRows returns the number of rows

            try
            {
                var emptyRow = Enumerable.Range(0, rows)
                    .Reverse()
                    .Where(row => GetCell(row, column) == 0)
                    .First();
                return emptyRow;
            }
            catch
            {
                return -1;
            }
        }

        /* playes token -> sets corresponding cell in grid */
        /* returns path to drop */
        public (List<bool>, int endingColumn) PlayMove(int startingColumn)
        {
            List<bool> path;
            int endingColumn ;
            int changedRow;
            int player;


            (path, endingColumn) = GetDropPath(startingColumn);

            changedRow = GetMaxRow(endingColumn);
            player = GetPlayer();
            SetCell(changedRow, endingColumn, player);
            return (path, endingColumn);
        }

        /* check if player has connected horizontaly */
        public bool CheckWinHorizontal(int player)
        {
            for (int row = 0; row < GetGridRows(); row++)
            {
                for (int col = 0; col <= GetGridColumns() - 4; col++)
                {
                    if (GetCell(row, col) == player &&
                        GetCell(row, col + 1) == player &&
                        GetCell(row, col + 2) == player &&
                        GetCell(row, col + 3) == player)
                    {
                        return true; // Horizontal win
                    }
                }
            }

            return false;
        }

        /* check if player has connected  verticaly*/
        public bool CheckWinVertical(int player)
        {
            for (int row = 0; row <= GetGridRows() - 4; row++)
            {
                for (int col = 0; col < GetGridColumns(); col++)
                {
                    if (GetCell(row, col) == player &&
                        GetCell(row + 1, col) == player &&
                        GetCell(row + 2, col) == player &&
                        GetCell(row + 3, col) == player)
                    {
                        return true; // Vertical win
                    }
                }
            }

            return false;
        }

        /* check if player has connected diagonaly */
        public bool CheckWinDiagonal(int player)
        {
            for (int row = 0; row <= GetGridRows() - 4; row++)
            {
                for (int col = 0; col <= GetGridColumns() - 4; col++)
                {
                    if (GetCell(row, col) == player &&
                        GetCell(row + 1, col + 1) == player &&
                        GetCell(row + 2, col + 2) == player &&
                        GetCell(row + 3, col + 3) == player)
                    {
                        return true;
                    }
                }
            }

            for (int row = 0; row <= GetGridRows() - 4; row++)
            {
                for (int col = GetGridColumns() - 1; col >= 3; col--)
                {
                    if (GetCell(row, col) == player &&
                        GetCell(row + 1, col - 1) == player &&
                        GetCell(row + 2, col - 2) == player &&
                        GetCell(row + 3, col - 3) == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /* returns true if current player has won */
        public bool CheckForWin()
        {
            int player = GetPlayer();
            if (CheckWinHorizontal(player)) return true;
            if(CheckWinVertical(player)) return true;
            if(CheckWinDiagonal(player)) return true;

            return false;  
        }
    }
}
