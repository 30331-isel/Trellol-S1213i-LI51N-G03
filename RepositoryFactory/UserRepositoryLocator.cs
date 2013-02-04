using System;
using System.Collections.Generic;
using RepositoryFactory.Exceptions;
using RepositoryInterfaces;
using DAL;

namespace RepositoryFactory
{
    public class UserRepositoryLocator
    {
        private readonly static IDictionary<string, Action> _setRepo = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            {DataSources.Memory, () => RepositoryFactory.SetRepoType<IUserRepository, UserMemoryRepository>()}
        };

        private readonly static IUserRepository _repo;

        static UserRepositoryLocator()
        {
            string dataSource = Configuration.GetDataSource(DataSources.UsersDataSourceName, Properties.Settings.Default.UsersDataSource);
            if (!_setRepo.ContainsKey(dataSource)) throw new InvalidDataSourceException();

            _setRepo[dataSource].Invoke();
            _repo = RepositoryFactory.Make<IUserRepository>();
        }

        public static IUserRepository Get()
        {
            return _repo;
        }
    }
}
