using System;
using System.Collections.Generic;
using DAL;
using RepositoryFactory.Exceptions;
using RepositoryInterfaces;

namespace RepositoryFactory
{
    public class CardRepositoryLocator
    {
        private readonly static IDictionary<string, Action> _setRepo = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            {DataSources.Memory, () => RepositoryFactory.SetRepoType<ICardRepository, CardMemoryRepository>()}
        };

        private readonly static ICardRepository _repo;

        static CardRepositoryLocator()
        {
            string dataSource = Configuration.GetDataSource(DataSources.CardsDataSourceName, Properties.Settings.Default.CardsDataSource);
            if (!_setRepo.ContainsKey(dataSource)) throw new InvalidDataSourceException();

            _setRepo[dataSource].Invoke();
            _repo = RepositoryFactory.Make<ICardRepository>();
        }

        public static ICardRepository Get()
        {
            return _repo;
        }
    }
}
