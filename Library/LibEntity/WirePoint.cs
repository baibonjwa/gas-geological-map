using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class WirePoint : ActiveRecordBase<WirePoint>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [Property]
        public double bottom_distance { get; set; }

        [Property]
        public double top_distance { get; set; }

        [BelongsTo("id")]
        public Wire wire { get; set; }

        [Property]
        public double coordinate_x { get; set; }

        [Property]
        public double coordinate_y { get; set; }

        [Property]
        public double coordinate_z { get; set; }

        [Property]
        public double left_distance { get; set; }

        [Property]
        public double right_distance { get; set; }

        [Property]
        public string bid { get; set; }
    }
}