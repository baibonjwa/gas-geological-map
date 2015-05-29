using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("wires")]
    public class Wire : ActiveRecordBase<Wire>
    {
        public const string TABLE_NAME = "T_WIRE_INFO";

        /// <summary>
        ///     导线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [HasMany(typeof(WirePoint), Table = "wire_points", ColumnKey = "wire_id",
    Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<WirePoint> wire_points { get; set; }

        /// <summary>
        ///     校核日期
        /// </summary>
        [Property]
        public DateTime check_date { get; set; }

        /// <summary>
        ///     校核者
        /// </summary>
        [Property]
        public string checker { get; set; }

        /// <summary>
        ///     计算日期
        /// </summary>
        [Property]
        public DateTime count_date { get; set; }

        /// <summary>
        ///     计算者
        /// </summary>
        [Property]
        public string counter { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("tunnel_id")]
        public Tunnel tunnel { get; set; }

        /// <summary>
        ///     导线名称
        /// </summary>
        [Property]
        public string name { get; set; }

        /// <summary>
        ///     导线级别
        /// </summary>
        [Property]
        public string level { get; set; }

        /// <summary>
        ///     测试日期
        /// </summary>
        [Property]
        public DateTime measure_date { get; set; }

        /// <summary>
        ///     观测者
        /// </summary>
        [Property]
        public string observer { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}