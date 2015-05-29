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
        public int id { get; set; }

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
        ///     绝对瓦斯涌出量
        /// </summary>
        [Property]
        public double absolute_gas_gush_quantity { get; set; }

        /// <summary>
        ///     相对瓦斯涌出量
        /// </summary>
        [Property]
        public double relative_gas_gush_quantity { get; set; }

        /// <summary>
        ///     工作面日产量
        /// </summary>
        [Property]
        public double working_face_day_output { get; set; }

        /// <summary>
        ///     回采年月
        /// </summary>
        [Property]
        public DateTime stope_date { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TunnelId")]
        public Tunnel tunnel { get; set; }

        /// <summary>
        ///     煤层编号
        /// </summary>
        [Property]
        public string coal_seam { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}