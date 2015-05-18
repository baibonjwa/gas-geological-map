using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Faultage : ActiveRecordBase<Faultage>
    {

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int faultage_id { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property]
        public string faultage_name { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property]
        public string gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property]
        public double angle { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Property]
        public string type { get; set; }

        /// <summary>
        ///     走向
        /// </summary>
        [Property]
        public double trend { get; set; }

        /// <summary>
        ///     断距
        /// </summary>
        [Property]
        public string separation { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [Property]
        public double length { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double coordinate_x { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property]
        public double coordinate_y { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property]
        public double coordinate_z { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        public static bool exists_by_faultage_name(string faultageName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("FaultageName", faultageName)
            };
            return Exists(criterion.ToArray());
        }

        public static Faultage find_by_faultage_name(string faultageName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("FaultageName", faultageName)
            };
            return FindOne(criterion.ToArray());
        }
    }
}