using Connect4.DAL.DataModels;


namespace Connect4.DAL.Repositories.Interfaces
{
    public interface IGridStandardRepository
    {
        GridModelStandard GetGridModel();
        void UpdateGridModel(GridModelStandard gridModel);
    }
}
