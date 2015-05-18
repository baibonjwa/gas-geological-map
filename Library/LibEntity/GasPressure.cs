using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord]
    public class GasPressure : ActiveRecordBase<GasPressure>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int gas_pressure_id { get; set; }

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
        ///     埋深
        /// </summary>
        [Property]
        public double depth { get; set; }

        /// <summary>
        ///     瓦斯压力值
        /// </summary>
        [Property]
        public double gas_pressure_value { get; set; }

        /// <summary>
        ///     测定时间
        /// </summary>
        [Property]
        public DateTime measure_date_time { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TunnelId")]
        public Tunnel tunnel { get; set; }

        /// <summary>
        ///     煤层编号
        /// </summary>
        //[BelongsTo("CoalSeamsId")]
        public CoalSeams coal_seams { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }
    }
}