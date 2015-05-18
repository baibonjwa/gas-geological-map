using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Borehole : ActiveRecordBase<Borehole>
    {
        /// <summary>
        ///     钻孔编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int borehole_id { get; set; }

        [HasMany(typeof(BoreholeLithology), Table = "T_BOREHOLE_LITHOLOGY", ColumnKey = "BoreholeId",
Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<BoreholeLithology> borehole_lithologys { get; set; }

        /// <summary>
        ///     孔号
        /// </summary>
        [Property]
        public string borehole_number { get; set; }

        /// <summary>
        ///     地面标高
        /// </summary>
        [Property]
        public double ground_elevation { get; set; }

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

        /// <summary>
        ///     煤层结构
        /// </summary>
        [Property]
        public string coal_seams_texture { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        public override void Delete()
        {
            BoreholeLithology.delete_all_by_borehole_id(borehole_id);
            base.Delete();
        }

        public static Borehole find_one_by_borehole_num(string boreholeNum)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("BoreholeNumber", boreholeNum)
            };
            return FindOne(criterion.ToArray());
        }
    }
}