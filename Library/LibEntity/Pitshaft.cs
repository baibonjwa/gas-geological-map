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
        public int PitshaftId { get; set; }

        /// <summary>
        ///     井筒名称
        /// </summary>
        [Property]
        public string PitshaftName { get; set; }

        /// <summary>
        ///     井筒类型
        /// </summary>
        [BelongsTo("PitshaftTypeId")]
        public PitshaftType PitshaftType { get; set; }

        /// <summary>
        ///     井口标高
        /// </summary>
        [Property]
        public double WellheadElevation { get; set; }

        /// <summary>
        ///     井底标高
        /// </summary>
        [Property]
        public double WellbottomElevation { get; set; }

        /// <summary>
        ///     井筒坐标X
        /// </summary>
        [Property]
        public double PitshaftCoordinateX { get; set; }

        /// <summary>
        ///     井筒坐标Y
        /// </summary>
        [Property]
        public double PitshaftCoordinateY { get; set; }

        /// <summary>
        ///     图形坐标X
        /// </summary>
        [Property]
        public double FigureCoordinateX { get; set; }

        /// <summary>
        ///     图形坐标Y
        /// </summary>
        [Property]
        public double FigureCoordinateY { get; set; }

        /// <summary>
        ///     图形坐标ZZ
        /// </summary>
        [Property]
        public double FigureCoordinateZ { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }

        public static bool ExistsByPitshaftName(string pitshaftName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("PitshaftName", pitshaftName)
            };
            return Exists(criterion.ToArray());
        }
    }
}