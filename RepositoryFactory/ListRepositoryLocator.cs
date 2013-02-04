using DAL;
using RepositoryFactory.Exceptions;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryFactory
{
    public class ListRepositoryLocator
    {
        private readonly static IDictionary<string, Action> _setRepo = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            {DataSources.Memory, () => RepositoryFactory.SetRepoType<IListRepository, ListMemoryRepository>()}
        };

        private readonly static IListRepository _repo;

        static ListRepositoryLocator()
        {
            string dataSource = Configuration.GetDataSource(DataSources.ListsDataSourceName, Properties.Settings.Default.ListsDataSource);
            if (!_setRepo.ContainsKey(dataSource)) throw new InvalidDataSourceException();

            _setRepo[dataSource].Invoke();
            _repo = RepositoryFactory.Make<IListRepository>();
        }

        public static IListRepository Get()
        {
            return _repo;
        }
    }
}
