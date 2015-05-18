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
        public int collapse_pillars_id { get; set; }


        [HasMany(typeof(CollapsePillarsPoint), Table = "T_COLLAPSE_PILLARS_POINT_INFO", ColumnKey = "CollapsePillarsId",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<CollapsePillarsPoint> collapse_pillars_points { get; set; }

        /// <summary>
        ///     设置或获取陷落柱名称
        /// </summary>
        [Property]
        public string collapse_pillars_name { get; set; }

        /// <summary>
        ///     设置或获取描述
        /// </summary>
        [Property]
        public string discribe { get; set; }

        /// <summary>
        ///     关键点ID
        /// </summary>
        public int point_id { get; set; }

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        public double coordinate_x { get; set; }

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        public double coordinate_y { get; set; }

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        public double coordinate_z { get; set; }

        /// <summary>
        ///     bindingID
        /// </summary>
        public string binding_id { get; set; }

        /// <summary>
        ///     类别
        /// </summary>
        public string xtype { get; set; }

        public static bool exists_by_collapse_pillars_name(string collapsePillarsName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("CollapsePillarsName", collapsePillarsName)
            };
            return Exists(criterion.ToArray());
        }

        public static CollapsePillars find_one_by_collapse_pillars_name(string collapsePillarsName)
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
        public int point_id { get; set; }

        /// <summary>
        ///     设置或获取陷落柱ID
        /// </summary>
        [BelongsTo("CollapsePillarsId")]
        public CollapsePillars collapse_pillars { get; set; }

        //关键点坐标X

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        [Property]
        public double coordinate_x { get; set; }

        //关键点坐标Y

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        [Property]
        public double coordinate_y { get; set; }

        //关键点坐标Z

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        [Property]
        public double coordinate_z { get; set; }

        //BID

        /// <summary>
        ///     绑定ID
        /// </summary>
        [Property]
        public string binding_id { get; set; }


        public static CollapsePillarsPoint[] find_all_by_collapse_pillars_id(int collapsePillarsId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("CollapsePillars.WirePointName", collapsePillarsId)
            };
            return FindAll(criterion.ToArray());
        }

        public static void delete_all_by_collapse_pillars_id(int collapsePillarsId)
        {
            DeleteAll(find_all_by_collapse_pillars_id(collapsePillarsId).Select(u => u.point_id));
        }
    }
}