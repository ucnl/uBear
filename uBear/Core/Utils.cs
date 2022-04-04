using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uBear.Core
{
    public static class Utils
    {
        #region Methods

        public static double O2D(object o)
        {
            return (o == null) ? double.NaN : (double)o;
        }

        public static int O2I(object o)
        {
            return (o == null) ? -1 : (int)o;
        }

        #endregion
    }
}
