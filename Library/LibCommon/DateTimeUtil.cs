
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCommon
{
    public class DateTimeUtil
    {
        /// <summary>
        /// Validate DateTimePicker date time
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime validateDTPDateTime(DateTime dt)
        {
            DateTime minimumDt = System.Windows.Forms.DateTimePicker.MinimumDateTime;

            if (DateTime.Compare(minimumDt, minimumDt) > 0)
                return dt;
            else
                return minimumDt;
        }
    }
}
