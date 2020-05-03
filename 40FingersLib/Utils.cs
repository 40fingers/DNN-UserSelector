using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortyFingers.Library
{
    /// <summary>
    /// Utility class containing several commonly used procedures by 40FINGERS
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Concatenates the values in a List Of Strings, sepratated by the supplied separator.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator">Any string</param>
        /// <returns></returns>
        public static string ToString(this List<string> list, string separator)
        {
            var retval = "";
            if (list != null && list.Count > 0)
            {
                foreach (var value in list)
                {
                    if (retval.Length > 0) retval += separator;
                    retval += value;
                }
            }

            return retval;
        }

    }
}