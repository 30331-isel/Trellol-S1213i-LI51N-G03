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
    public class RoleRepositoryLocator
    {
        private readonly static IDictionary<string, Action> _setRepo = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            {DataSources.Memory, () => RepositoryFactory.SetRepoType<IRoleRepository, RoleMemoryRepository>()}
        };

        private readonly static IRoleRepository _repo;

        static RoleRepositoryLocator()
        {
            string dataSource = Configuration.GetDataSource(DataSources.RolesDataSourceName, Properties.Settings.Default.BoardsDataSource);
            if (!_setRepo.ContainsKey(dataSource)) throw new InvalidDataSourceException();
            
            _setRepo[dataSource].Invoke();
            _repo = RepositoryFactory.Make<IRoleRepository>();
        }

        public static IRoleRepository Get()
        {
            return _repo;
        }
    }
}
