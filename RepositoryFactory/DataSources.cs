using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryFactory
{
    class DataSources
    {
        private static string[] _dataSourceNames = new string[5]{
            "BoardsDataSource", "ListsDataSource", "CardsDataSource", "UsersDataSource", "RolesDataSource"
        };

        /* Data Sources */
        private static string[] _dataSources = new string[1]{
            "Memory"
        };

        internal static string Memory { get { return _dataSources[0]; } }

        internal static string BoardsDataSourceName { get { return _dataSourceNames[0]; } }
        internal static string ListsDataSourceName { get { return _dataSourceNames[1]; } }
        internal static string CardsDataSourceName { get { return _dataSourceNames[2]; } }
        internal static string UsersDataSourceName { get { return _dataSourceNames[3]; } }
        internal static string RolesDataSourceName { get { return _dataSourceNames[4]; } }

        internal static bool ContainsDataSource(string dataSourceName, string dataSourceValue)
        {
            if(!_dataSourceNames.Contains(dataSourceName, StringComparer.OrdinalIgnoreCase)) 
                throw new ArgumentException("Invalid Data Source Name");
            if (_dataSources.Contains(dataSourceValue, StringComparer.OrdinalIgnoreCase)) 
                return true;

            return false;
        }

    }
}
