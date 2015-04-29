using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCommon
{
    public class TunnelFilter
    {
        public enum TunnelFilterRules
        {
            /**绑定导线的巷道**/
            IS_WIRE_INFO_BIND,
            /**为切眼的巷道**/
            IS_TUNNE_KQY
        }
    }
}
