using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class SubBorehole : ActiveRecordBase<SubBorehole>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        /// <summary>
        ///     钻孔编号
        /// </summary>
        [BelongsTo("id")]
        public Borehole borehole { get; set; }

        /// <summary>
        ///     岩性编号
        /// </summary>
        [Property]
        public string lithology { get; set; }

        /// <summary>
        ///     底板标高
        /// </summary>
        [Property]
        public double floor_elevation { get; set; }

        /// <summary>
        ///     厚度
        /// </summary>
        [Property]
        public double thickness { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property]
        public string coal_seam { get; set; }

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
    }
}