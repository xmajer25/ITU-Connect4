using Connect4.DAL.DataModels;


namespace Connect4.DAL.Repositories.Interfaces
{
    /*
    * Author   : Dušan Slúka
    * File     : IGridStandardRepository
    * Brief    : Interface for accessing and updating the 'GridModelStandard' data model.
    */

    public interface IGridStandardRepository
    {
        GridModelStandard GetGridModel();
        void UpdateGridModel(GridModelStandard gridModel);
    }
}
