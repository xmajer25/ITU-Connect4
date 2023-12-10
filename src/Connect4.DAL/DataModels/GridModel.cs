using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.DAL.DataModels
{
    public class GridModel
    {
        public readonly int Rows = 6;
        public readonly int Columns = 8;
        public readonly int[,] Grid;

        public GridModel()
        {
            Grid = new int[Rows, Columns];
            Array.Clear(Grid, 0, Grid.Length);
        }
    }
}
