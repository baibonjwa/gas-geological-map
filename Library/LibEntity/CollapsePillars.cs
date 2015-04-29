using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class CollapsePillars : ActiveRecordBase<CollapsePillars>
    {
        public const String TABLE_NAME = "T_COLLAPSE_PILLARS_INFO";

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int CollapsePillarsId { get; set; }


        [HasMany(typeof(CollapsePillarsPoint), Table = "T_COLLAPSE_PILLARS_POINT_INFO", ColumnKey = "CollapsePillarsId",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<CollapsePillarsPoint> CollapsePillarsPoints { get; set; }

        /// <summary>
        ///     设置或获取陷落柱名称
        /// </summary>
        [Property]
        public string CollapsePillarsName { get; set; }

        /// <summary>
        ///     设置或获取描述
        /// </summary>
        [Property]
        public string Discribe { get; set; }

        /// <summary>
        ///     关键点ID
        /// </summary>
        public int PointId { get; set; }

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        /// <summary>
        ///     bindingID
        /// </summary>
        public string BindingId { get; set; }

        /// <summary>
        ///     类别
        /// </summary>
        public string Xtype { get; set; }

        public static bool ExistsByCollapsePillarsName(string collapsePillarsName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("CollapsePillarsName", collapsePillarsName)
            };
            return Exists(criterion.ToArray());
        }

        public static CollapsePillars FindOneByCollapsePillarsName(string collapsePillarsName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("CollapsePillarsName", collapsePillarsName)
            };
            return FindOne(criterion.ToArray());
        }

    }

    /// <summary>
    ///     20140531 lyf
    ///     陷落柱关键点实体
    /// </summary>
    [ActiveRecord]
    public class CollapsePillarsPoint : ActiveRecordBase<CollapsePillarsPoint>
    {
        /// <summary>
        ///     关键点ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int PointId { get; set; }

        /// <summary>
        ///     设置或获取陷落柱ID
        /// </summary>
        [BelongsTo("CollapsePillarsId")]
        public CollapsePillars CollapsePillars { get; set; }

        //关键点坐标X

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        [Property]
        public double CoordinateX { get; set; }

        //关键点坐标Y

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        [Property]
        public double CoordinateY { get; set; }

        //关键点坐标Z

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        [Property]
        public double CoordinateZ { get; set; }

        //BID

        /// <summary>
        ///     绑定ID
        /// </summary>
        [Property]
        public string BindingId { get; set; }


        public static CollapsePillarsPoint[] FindAllByCollapsePillarsId(int collapsePillarsId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("CollapsePillars.WirePointName", collapsePillarsId)
            };
            return FindAll(criterion.ToArray());
        }

        public static void DeleteAllByCollapsePillarsId(int collapsePillarsId)
        {
            DeleteAll(FindAllByCollapsePillarsId(collapsePillarsId).Select(u => u.PointId));
        }
    }
}