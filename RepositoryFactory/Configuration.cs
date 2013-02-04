using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryFactory
{
    public class Configuration
    {

        
        internal static string GetDataSource(string DataSourceName, string DataSourceType)
        {
            if (DataSources.ContainsDataSource(DataSourceName, DataSourceType))
                return DataSourceType;
            /* Default Data Source */
            return DataSources.Memory;
        }

    }

}
