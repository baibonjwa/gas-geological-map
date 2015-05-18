using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class InferFaultage : ActiveRecordBase<InferFaultage>
    {
        public const string TABLE_NAME = "InferFaultage";
        public const string C_FAULTAGE_NAME = "BigFaultageName";

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [HasMany(typeof (InferFaultagePoint), Table = "InferFaultagePoints", ColumnKey = "id",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<InferFaultagePoint> big_faultage_points { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property]
        public string big_faultage_name { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property]
        public string gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property]
        public string angle { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Property]
        public string type { get; set; }

        /// <summary>
        ///     走向
        /// </summary>
        [Property]
        public string trend { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string bid { get; set; }

        public static InferFaultage find_one_by_big_faultage_name(string bigFaultageName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("BigFaultageName", bigFaultageName)
            };
            return FindOne(criterion);
        }
    }
}