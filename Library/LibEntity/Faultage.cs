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
        public int FaultageId { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property]
        public string FaultageName { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property]
        public string Gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property]
        public double Angle { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Property]
        public string Type { get; set; }

        /// <summary>
        ///     走向
        /// </summary>
        [Property]
        public double Trend { get; set; }

        /// <summary>
        ///     断距
        /// </summary>
        [Property]
        public string Separation { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [Property]
        public double Length { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double CoordinateX { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property]
        public double CoordinateY { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property]
        public double CoordinateZ { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }

        public static bool ExistsByFaultageName(string faultageName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("FaultageName", faultageName)
            };
            return Exists(criterion.ToArray());
        }

        public static Faultage FindByFaultageName(string faultageName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("FaultageName", faultageName)
            };
            return FindOne(criterion.ToArray());
        }
    }
}