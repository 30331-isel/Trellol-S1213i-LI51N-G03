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
    public class BoardRepositoryLocator
    {
        private readonly static IDictionary<string, Action> _setRepo = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            {DataSources.Memory, () => RepositoryFactory.SetRepoType<IBoardRepository, BoardMemoryRepository>()}
        };

        private readonly static IBoardRepository _repo;

        static BoardRepositoryLocator()
        {
            string dataSource = Configuration.GetDataSource(DataSources.BoardsDataSourceName, Properties.Settings.Default.BoardsDataSource);
            if (!_setRepo.ContainsKey(dataSource)) throw new InvalidDataSourceException();
            
            _setRepo[dataSource].Invoke();
            _repo = RepositoryFactory.Make<IBoardRepository>();
        }

        public static IBoardRepository Get()
        {
            return _repo;
        }
    }
}
