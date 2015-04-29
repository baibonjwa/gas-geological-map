using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class StopLine : ActiveRecordBase<StopLine>
    {
        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int StopLineId { get; set; }

        /// <summary>
        ///     设置或获取停采线名称
        /// </summary>
        [Property]
        public string StopLineName { get; set; }

        /// <summary>
        ///     设置或获取起点坐标X
        /// </summary>
        [Property]
        public double SCoordinateX { get; set; }

        /// <summary>y
        ///     设置或获取起点坐标Y
        /// </summary>
        [Property]
        public double SCoordinateY { get; set; }

        /// <summary>
        ///     设置或获取起点坐标Z
        /// </summary>
        [Property]
        public double SCoordinateZ { get; set; }

        /// <summary>
        ///     设置或获取终点坐标X
        /// </summary>
        [Property]
        public double FCoordinateX { get; set; }

        /// <summary>
        ///     设置或获取终点坐标Y
        /// </summary>
        [Property]
        public double FCoordinateY { get; set; }

        /// <summary>
        ///     设置或获取终点坐标Z
        /// </summary>
        [Property]
        public double FCoordinateZ { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }

        public static bool ExistsByStopLineName(string stopLineName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("StopLineName", stopLineName)
            };
            return Exists(criterion.ToArray());
        }
    }
}