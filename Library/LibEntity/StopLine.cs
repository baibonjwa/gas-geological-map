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
        public int stop_line_id { get; set; }

        /// <summary>
        ///     设置或获取停采线名称
        /// </summary>
        [Property]
        public string stop_line_name { get; set; }

        /// <summary>
        ///     设置或获取起点坐标X
        /// </summary>
        [Property]
        public double s_coordinate_x { get; set; }

        /// <summary>y
        ///     设置或获取起点坐标Y
        /// </summary>
        [Property]
        public double s_coordinate_y { get; set; }

        /// <summary>
        ///     设置或获取起点坐标Z
        /// </summary>
        [Property]
        public double s_coordinate_z { get; set; }

        /// <summary>
        ///     设置或获取终点坐标X
        /// </summary>
        [Property]
        public double f_coordinate_x { get; set; }

        /// <summary>
        ///     设置或获取终点坐标Y
        /// </summary>
        [Property]
        public double f_coordinate_y { get; set; }

        /// <summary>
        ///     设置或获取终点坐标Z
        /// </summary>
        [Property]
        public double f_coordinate_z { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        public static bool exists_by_stop_line_name(string stopLineName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("StopLineName", stopLineName)
            };
            return Exists(criterion.ToArray());
        }
    }
}