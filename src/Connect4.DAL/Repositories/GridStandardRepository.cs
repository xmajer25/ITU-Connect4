using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories.Interfaces;
/*
* Author   : Dušan Slúka
* File     : GridModelStandard
* Brief    : A repository class for managing the standard game grid model.
*/
namespace Connect4.DAL.Repositories
{
    
    public class GridStandardRepository : IGridStandardRepository
    {
        // A private field holding the current state of the grid model.
        private GridModelStandard _gridModel;

        // Constructor that initializes a new grid model.
        public GridStandardRepository()
        {
            _gridModel = new GridModelStandard();
        }

        // Retrieves a copy of the current grid model.
        public GridModelStandard GetGridModel()
        {
            return new GridModelStandard(_gridModel);
        }

        // Updates the current grid model with a new state.
        public void UpdateGridModel(GridModelStandard gridModel)
        {
            _gridModel = gridModel;
        }
    }
}
