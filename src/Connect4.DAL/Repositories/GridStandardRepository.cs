using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories.Interfaces;

namespace Connect4.DAL.Repositories
{
    internal class GridStandardRepository : IGridStandardRepository
    {
        private GridModelStandard _gridModel;

        public GridStandardRepository()
        {
            _gridModel = new GridModelStandard();
        }

        public GridModelStandard GetGridModel()
        {
            return new GridModelStandard(_gridModel);
        }

        public void UpdateGridModel(GridModelStandard gridModel)
        {
            _gridModel = gridModel;
        }
    }
}
