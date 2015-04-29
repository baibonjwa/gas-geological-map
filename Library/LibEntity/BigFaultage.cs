using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class BigFaultage : ActiveRecordBase<BigFaultage>
    {

        public const String TableName = "BigFaultage";
        public const String CFaultageName = "BigFaultageName";

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int BigFaultageId { get; set; }


        [HasMany(typeof(BigFaultagePoint), Table = "BigFaultagePoint", ColumnKey = "BigFaultageId",
    Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<BigFaultagePoint> BigFaultagePoints { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property]
        public string BigFaultageName { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property]
        public string Gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property]
        public string Angle { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Property]
        public string Type { get; set; }

        /// <summary>
        ///     走向
        /// </summary>
        [Property]
        public string Trend { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }

        public static BigFaultage FindOneByBigFaultageName(string bigFaultageName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("BigFaultageName", bigFaultageName)
            };
            return FindOne(criterion);
        }

        //public override void Delete()
        //{
        //    var bigFaultagePoints = BigFaultagePoint.FindAllByFaultageId(BigFaultageId);
        //    BigFaultagePoint.DeleteAll(bigFaultagePoints.Select(u => u.StopLineId));
        //    base.Delete();
        //}
    }
}