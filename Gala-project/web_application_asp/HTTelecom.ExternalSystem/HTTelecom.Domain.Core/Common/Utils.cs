using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Common
{
    public class Utils
    {
        public static List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> sorter, string direction)
        {
            if (direction.ToLower().Equals("desc"))
            {
                list = list.OrderByDescending(sorter).ToList();
            }
            else
            {
                list = list.OrderBy(sorter).ToList();
            }
            return list;
        }
    }
}
