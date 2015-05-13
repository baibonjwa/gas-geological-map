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
        public int GasPressureId { get; set; }

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
        ///     埋深
        /// </summary>
        [Property]
        public double Depth { get; set; }

        /// <summary>
        ///     瓦斯压力值
        /// </summary>
        [Property]
        public double GasPressureValue { get; set; }

        /// <summary>
        ///     测定时间
        /// </summary>
        [Property]
        public DateTime MeasureDateTime { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TunnelId")]
        public Tunnel Tunnel { get; set; }

        /// <summary>
        ///     煤层编号
        /// </summary>
        //[BelongsTo("CoalSeamsId")]
        public CoalSeams CoalSeams { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }
    }
}