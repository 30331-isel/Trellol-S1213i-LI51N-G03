namespace RepositoryInterfaces
{
    using System.Collections.Generic;
    using EntitiesLogic.Entities;

    public interface IListRepository : IRepository<List> 
    {
        IEnumerable<List> GetListsFromBoard(string id);
    }
}