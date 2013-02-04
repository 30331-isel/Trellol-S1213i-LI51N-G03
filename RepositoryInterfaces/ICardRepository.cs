namespace RepositoryInterfaces
{
    using System.Collections.Generic;
    using EntitiesLogic.Entities;

    public interface ICardRepository : IRepository<Card> 
    {
        IEnumerable<Card> GetCardsFromBoard(string id);
        int Count(string board);
    }
}