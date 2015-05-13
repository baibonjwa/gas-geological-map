using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Wire : ActiveRecordBase<Wire>
    {
        public const String TableName = "T_WIRE_INFO";

        /// <summary>
        ///     导线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int WireId { get; set; }

        [HasMany(typeof(WirePoint), Table = "WirePoint", ColumnKey = "WireId",
    Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<WirePoint> WirePoints { get; set; }

        /// <summary>
        ///     校核日期
        /// </summary>
        [Property]
        public DateTime CheckDate { get; set; }

        /// <summary>
        ///     校核者
        /// </summary>
        [Property]
        public string Checker { get; set; }

        /// <summary>
        ///     计算日期
        /// </summary>
        [Property]
        public DateTime CountDate { get; set; }

        /// <summary>
        ///     计算者
        /// </summary>
        [Property]
        public string Counter { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TunnelId")]
        public Tunnel Tunnel { get; set; }

        /// <summary>
        ///     导线名称
        /// </summary>
        [Property]
        public string WireName { get; set; }

        /// <summary>
        ///     导线级别
        /// </summary>
        [Property]
        public string WireLevel { get; set; }

        /// <summary>
        ///     测试日期
        /// </summary>
        [Property]
        public DateTime MeasureDate { get; set; }

        /// <summary>
        ///     观测者
        /// </summary>
        [Property]
        public string Vobserver { get; set; }

        public override void Delete()
        {
            var wirePoints = WirePoint.FindAllByWireId(WireId);
            foreach (var p in wirePoints)
            {
                p.Delete();
            }
            base.Delete();
        }

        public static Wire FindOneByTunnelId(int tunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId",tunnelId)
            };
            return FindOne(criterion);
        }

        public static void DeleteByTunnelId(int tunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId",tunnelId)
            };
            Delete(criterion);
        }


    }
}