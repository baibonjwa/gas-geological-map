using System;

namespace LibCommon
{
    public class IdGenerator
    {
        /// <summary>
        /// 生成新的绑定ID（GUID）
        /// </summary>
        /// <returns>返回生成的绑定ID（全球唯一)</returns>
        static public string NewBindingId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
