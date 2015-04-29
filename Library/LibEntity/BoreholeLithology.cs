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
        public int BoreholeLithhologyId { get; set; }

        /// <summary>
        ///     钻孔编号
        /// </summary>
        [BelongsTo("BoreholeId")]
        public Borehole Borehole { get; set; }

        /// <summary>
        ///     岩性编号
        /// </summary>
        [BelongsTo]
        public Lithology Lithology { get; set; }

        /// <summary>
        ///     底板标高
        /// </summary>
        [Property]
        public double FloorElevation { get; set; }

        /// <summary>
        ///     厚度
        /// </summary>
        [Property]
        public double Thickness { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property]
        public string CoalSeamsName { get; set; }

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

        public static BoreholeLithology[] FindAllByBoreholeId(int boreholeId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Borehole.BoreholeId", boreholeId) };
            return FindAll(criterion.ToArray());
        }

        public static void DeleteAllByBoreholeId(int boreholeId)
        {
            DeleteAll(FindAllByBoreholeId(boreholeId).Select(u => u.BoreholeLithhologyId));
        }
    }
}