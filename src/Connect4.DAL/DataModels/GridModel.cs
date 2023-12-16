using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Author   : Jakub Majer (xmajer25)
 * File     : GridModel
 * Brief    : Model used for grid in connect4. 1D array for xml parsing.
 */

namespace Connect4.DAL.DataModels
{
    public class GridModel
    {
        public int Rows = 6;
        public int Columns = 8;
        public int[] Grid;
        public int CurrentPlayer;

        public GridModel()
        {
            Grid = new int[Rows * Columns];
            Array.Clear(Grid, 0, Grid.Length);
            CurrentPlayer = 1;
        }
    }
}
