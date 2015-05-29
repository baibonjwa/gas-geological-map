using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("collapse_pillars")]
    public class CollapsePillar : ActiveRecordBase<CollapsePillar>
    {
        public const string TABLE_NAME = "T_COLLAPSE_PILLARS_INFO";

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }


        [HasMany(typeof(CollapsePillarPoint), Table = "collapse_pillar_points", ColumnKey = "collapse_pillar_id",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<CollapsePillarPoint> collapse_pillar_points { get; set; }

        /// <summary>
        ///     设置或获取陷落柱名称
        /// </summary>
        [Property]
        public string name { get; set; }

        /// <summary>
        ///     设置或获取描述
        /// </summary>
        [Property]
        public string discribe { get; set; }

        /// <summary>
        ///     bindingID
        /// </summary>
        [Property]
        public string bid { get; set; }

        /// <summary>
        ///     类别
        /// </summary>
        public string xtype { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;


    }

    /// <summary>
    ///     20140531 lyf
    ///     陷落柱关键点实体
    /// </summary>
    [ActiveRecord("collapse_pillar_points")]
    public class CollapsePillarPoint : ActiveRecordBase<CollapsePillarPoint>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [BelongsTo("collapse_pillar_id")]
        public CollapsePillar collapse_pillar { get; set; }

        [Property]
        public double coordinate_x { get; set; }

        [Property]
        public double coordinate_y { get; set; }

        [Property]
        public double coordinate_z { get; set; }

        [Property]
        public string bid { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}