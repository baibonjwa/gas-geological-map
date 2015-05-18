using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    public class Pitshaft : ActiveRecordBase<Pitshaft>
    {
        /// <summary>
        ///     井筒编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int pitshaft_id { get; set; }

        /// <summary>
        ///     井筒名称
        /// </summary>
        [Property]
        public string pitshaft_name { get; set; }

        /// <summary>
        ///     井筒类型
        /// </summary>
        [BelongsTo("PitshaftTypeId")]
        public PitshaftType pitshaft_type { get; set; }

        /// <summary>
        ///     井口标高
        /// </summary>
        [Property]
        public double wellhead_elevation { get; set; }

        /// <summary>
        ///     井底标高
        /// </summary>
        [Property]
        public double wellbottom_elevation { get; set; }

        /// <summary>
        ///     井筒坐标X
        /// </summary>
        [Property]
        public double pitshaft_coordinate_x { get; set; }

        /// <summary>
        ///     井筒坐标Y
        /// </summary>
        [Property]
        public double pitshaft_coordinate_y { get; set; }

        /// <summary>
        ///     图形坐标X
        /// </summary>
        [Property]
        public double figure_coordinate_x { get; set; }

        /// <summary>
        ///     图形坐标Y
        /// </summary>
        [Property]
        public double figure_coordinate_y { get; set; }

        /// <summary>
        ///     图形坐标ZZ
        /// </summary>
        [Property]
        public double figure_coordinate_z { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        public static bool exists_by_pitshaft_name(string pitshaftName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("PitshaftName", pitshaftName)
            };
            return Exists(criterion.ToArray());
        }
    }
}