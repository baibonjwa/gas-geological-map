using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord]
    public class GasContent : ActiveRecordBase<GasContent>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public double coordinate_x { get; set; }

        [Property]
        public double coordinate_y { get; set; }

        [Property]
        public double coordinate_z { get; set; }

        [Property]
        public double depth { get; set; }

        [Property]
        public double gas_content_value { get; set; }

        [Property]
        public DateTime measure_datetime { get; set; }

        [BelongsTo("TunnelId")]
        public Tunnel tunnel { get; set; }

        [Property]
        public string coal_seam { get; set; }

        [Property]
        public string bid { get; set; }
    }
}