using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord]
    public class GasGushQuantity : ActiveRecordBase<GasGushQuantity>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int GasGushQuantityId { get; set; }

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
        ///     绝对瓦斯涌出量
        /// </summary>
        [Property]
        public double AbsoluteGasGushQuantity { get; set; }

        /// <summary>
        ///     相对瓦斯涌出量
        /// </summary>
        [Property]
        public double RelativeGasGushQuantity { get; set; }

        /// <summary>
        ///     工作面日产量
        /// </summary>
        [Property]
        public double WorkingFaceDayOutput { get; set; }

        /// <summary>
        ///     回采年月
        /// </summary>
        [Property]
        public DateTime StopeDate { get; set; }

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