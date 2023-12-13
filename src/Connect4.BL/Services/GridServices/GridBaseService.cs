using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.BL.Services.GridServices
{
    public class GridBaseService
    {
        private readonly GridModel GridData;

        public GridBaseService()
        {
            GridData = new GridModel();
        }

        public int[,] GetGrid()
            => GridData.Grid;
        

        public int GetGridRows()
            => GridData.Rows;
        

        public int GetGridColumns()
            => GridData.Columns;

        public int GetPlayer()
            => GridData.CurrentPlayer;
        

        public int GetCell(int row, int column)
            => GetGrid()[row, column];
       

        public void SetCell(int row, int column, int value)
            => GetGrid()[row, column] = value;

        public void SwapPlayers()
            => GridData.CurrentPlayer = (GridData.CurrentPlayer == 1 ? 2 : 1);
        
    }
}
