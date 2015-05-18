using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Wire : ActiveRecordBase<Wire>
    {
        public const String TABLE_NAME = "T_WIRE_INFO";

        /// <summary>
        ///     导线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int wire_id { get; set; }

        [HasMany(typeof(WirePoint), Table = "WirePoint", ColumnKey = "WireId",
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
        [BelongsTo("TunnelId")]
        public Tunnel tunnel { get; set; }

        /// <summary>
        ///     导线名称
        /// </summary>
        [Property]
        public string wire_name { get; set; }

        /// <summary>
        ///     导线级别
        /// </summary>
        [Property]
        public string wire_level { get; set; }

        /// <summary>
        ///     测试日期
        /// </summary>
        [Property]
        public DateTime measure_date { get; set; }

        /// <summary>
        ///     观测者
        /// </summary>
        [Property]
        public string vobserver { get; set; }

        public override void Delete()
        {
            var wirePoints = WirePoint.find_all_by_wire_id(wire_id);
            foreach (var p in wirePoints)
            {
                p.Delete();
            }
            base.Delete();
        }

        public static Wire find_one_by_tunnel_id(int tunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId",tunnelId)
            };
            return FindOne(criterion);
        }

        public static void delete_by_tunnel_id(int tunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId",tunnelId)
            };
            Delete(criterion);
        }


    }
}