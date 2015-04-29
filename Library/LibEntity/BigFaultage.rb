using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_BIG_FAULTAGE")]
    public class BigFaultage : ActiveRecordBase<BigFaultage>
    {

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "FAULTAGE_ID")]
        public int FaultageId { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property("FAULTAGE_NAME")]
        public string FaultageName { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property("GAP")]
        public string Gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property("ANGLE")]
        public string Angle { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Property("TYPE")]
        public string Type { get; set; }

        /// <summary>
        ///     走向
        /// </summary>
        [Property("TREND")]
        public string Trend { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }
    }
}