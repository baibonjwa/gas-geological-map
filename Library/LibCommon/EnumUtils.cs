using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCommon
{
    public class EnumUtils
    {

        /// <summary>
        /// int --> enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetEnumInstance<T>(T enumType, string value)
        {

            T returnValue = (T)Enum.Parse(typeof(T), value);
            return returnValue;
        }
    }
}
