using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : GridBaseService
 * Brief    : Service for grid. Simple setters and getters. 
 */

namespace Connect4.BL.Services.GridServices
{
    public class GridBaseService
    {
        public GridRepository GridData;

        public GridBaseService()
        {
            GridData = new GridRepository();
        }

        public int[] GetGrid()
            => GridData.GetGrid();
        

        public int GetGridRows()
            => GridData.GetRows();
        

        public int GetGridColumns()
            => GridData.GetColumns();

        public int GetPlayer()
            => GridData.GetPlayer();
        

        public int GetCell(int row, int column)
            => GridData.GetValue(row, column);
       

        public void SetCell(int row, int column, int value)
            => GridData.SetCell(row, column, value);
        
            

        public void SwapPlayers()
        {
            int player = GetPlayer();
            player = (player == 1 ? 2 : 1);
            GridData.SetPlayer(player);
        }
    }
}
