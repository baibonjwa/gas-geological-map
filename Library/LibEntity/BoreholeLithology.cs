using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class BoreholeLithology : ActiveRecordBase<BoreholeLithology>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int borehole_lithhology_id { get; set; }

        /// <summary>
        ///     钻孔编号
        /// </summary>
        [BelongsTo("BoreholeId")]
        public Borehole borehole { get; set; }

        /// <summary>
        ///     岩性编号
        /// </summary>
        [BelongsTo]
        public Lithology lithology { get; set; }

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
        public string coal_seams_name { get; set; }

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

        public static BoreholeLithology[] find_all_by_borehole_id(int boreholeId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Borehole.BoreholeId", boreholeId) };
            return FindAll(criterion.ToArray());
        }

        public static void delete_all_by_borehole_id(int boreholeId)
        {
            DeleteAll(find_all_by_borehole_id(boreholeId).Select(u => u.borehole_lithhology_id));
        }
    }
}