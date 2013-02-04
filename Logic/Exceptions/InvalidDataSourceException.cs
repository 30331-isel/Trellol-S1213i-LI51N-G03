using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    class InvalidDataSourceException : System.Exception
    {
        private string _p;

        public InvalidDataSourceException()
        {

        }

        public InvalidDataSourceException(string p)
        {
            _p = p;
        }


    }
}
