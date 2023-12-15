using Connect4.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : GridRepository
 * Brief    : Repository for grid model
 */

namespace Connect4.DAL.Repositories
{
    public class GridRepository
    {
        public GridModel Grid;
        private static string filePath = "/Storage/gridData.xml";


        /* get new grid in constructor */
        public GridRepository()
        {
            if (File.Exists(filePath))
            {
                Grid = Load();

                if (IsGridEmpty(Grid))
                    Grid = new GridModel();
            }
            else
            {
                Grid = new GridModel();
            }
            Save();
        }

        /* returns grid */
        public int[] GetGrid()
        {
            Grid = Load();
            return Grid.Grid;
        }

        /* returns max ammount of rows in grid */
        public int GetRows()
        {
            Grid = Load();
            return Grid.Rows;
        }

        /* returns max ammount of columns in grid */
        public int GetColumns()
        {
            Grid = Load();
            return Grid.Columns;
        }

        /* computes index from row and column into 1d array index */
        public int GetIndex(int row, int col)
        {
            return row * GetColumns() + col;
        }

        /* returns value of a cell inside grid */
        public int GetValue(int row, int col)
        {
            Grid = Load();
            int index = GetIndex(row, col);
            return Grid.Grid[index];
        }


        /* sets cell to a value */
        public void SetCell(int row, int col, int value)
        {
            Grid = Load();
            int index = GetIndex(row, col);
            Grid.Grid[index] = value;
            Save();
        }

        /* returns current player */
        public int GetPlayer()
        {
            Grid = Load();
            return Grid.CurrentPlayer;
        }

        /* sets player */
        public void SetPlayer(int player)
        {
            Grid = Load();
            Grid.CurrentPlayer = player;
            Save();
        }

        /* write grid data into xml file */
        public void Save()
        {
            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage", "gridData.xml");

                // Ensure the directory exists
                string directoryPath = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(GridModel));
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, Grid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving file: " + ex.Message);
            }
        }

        /* load data from xml file */
        public static GridModel Load()
        {
            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage", "gridData.xml");

                if (File.Exists(fullPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(GridModel));
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                    {
                        return (GridModel)serializer.Deserialize(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
            }

            return null;
        }

        /*check if grid is empty*/
        private bool IsGridEmpty(GridModel grid)
        {
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    if (GetValue(i, j) != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
