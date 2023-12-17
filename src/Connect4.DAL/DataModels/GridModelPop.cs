using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Ivan Mahut (xmahut01)
 * File     : GridModelPop
 * Brief    : Data model to store information about PopOut game mode
 *            It uses array of size row * column to store player tokens
 */

namespace Connect4.DAL.DataModels
{
    public class GridModelPop
    {
        public int Id { get; set; }
        public readonly int Rows = 6;
        public readonly int Columns = 8;
        public int[] Grid;
        public int CurrentPlayer;

        public GridModelPop()
        {
            Grid = new int[Rows * Columns];

            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i] = 0;
            }

            CurrentPlayer = 1;
        }
    }
}
