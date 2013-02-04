using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUtils
    {
        internal static void AdjustPosition<TSource>(
            IEnumerable<TSource> list, 
            //Func<TSource, bool> equal, 
            Func<TSource, bool> greaterOrEqual, 
            Action<TSource> inc)
        {
            
          //  if (list.SingleOrDefault(equal) != null)
          // {
                foreach (TSource item in list.Where(greaterOrEqual))
                    inc(item);
                return;
          //  }
        }
    }
}
