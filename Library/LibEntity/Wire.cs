using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_WIRE_INFO")]
    public class Wire : ActiveRecordBase<Wire>
    {
        public const String TableName = "T_WIRE_INFO";

        /// <summary>
        ///     导线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "OBJECTID")]
        public int WireId { get; set; }

        [HasMany(typeof(WirePoint), Table = "T_WIRE_POINT", ColumnKey = "WIRE_INFO_ID",
    Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<WirePoint> WirePoints { get; set; }

        /// <summary>
        ///     校核日期
        /// </summary>
        [Property("CHECK_DATE")]
        public DateTime CheckDate { get; set; }

        /// <summary>
        ///     校核者
        /// </summary>
        [Property("CHECKER")]
        public string Checker { get; set; }

        /// <summary>
        ///     计算日期
        /// </summary>
        [Property("COUNT_DATE")]
        public DateTime CountDate { get; set; }

        /// <summary>
        ///     计算者
        /// </summary>
        [Property("COUNTER")]
        public string Counter { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }

        /// <summary>
        ///     导线名称
        /// </summary>
        [Property("WIRE_NAME")]
        public string WireName { get; set; }

        /// <summary>
        ///     导线级别
        /// </summary>
        [Property("WIRE_LEVEL")]
        public string WireLevel { get; set; }

        /// <summary>
        ///     测试日期
        /// </summary>
        [Property("MEASURE_DATE")]
        public DateTime MeasureDate { get; set; }

        /// <summary>
        ///     观测者
        /// </summary>
        [Property("VOBSERVER")]
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