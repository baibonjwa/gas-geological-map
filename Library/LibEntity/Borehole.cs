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
        public int BoreholeId { get; set; }

        [HasMany(typeof(BoreholeLithology), Table = "T_BOREHOLE_LITHOLOGY", ColumnKey = "BoreholeId",
Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<BoreholeLithology> BoreholeLithologys { get; set; }

        /// <summary>
        ///     孔号
        /// </summary>
        [Property]
        public string BoreholeNumber { get; set; }

        /// <summary>
        ///     地面标高
        /// </summary>
        [Property]
        public double GroundElevation { get; set; }

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
        ///     煤层结构
        /// </summary>
        [Property]
        public string CoalSeamsTexture { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }

        public override void Delete()
        {
            BoreholeLithology.DeleteAllByBoreholeId(BoreholeId);
            base.Delete();
        }

        public static Borehole FindOneByBoreholeNum(string boreholeNum)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("BoreholeNumber", boreholeNum)
            };
            return FindOne(criterion.ToArray());
        }
    }
}