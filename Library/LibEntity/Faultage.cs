using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Faultage : ActiveRecordBase<Faultage>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [Property]
        public string gap { get; set; }

        [Property]
        public double angle { get; set; }

        [Property]
        public string type { get; set; }

        [Property]
        public double trend { get; set; }

        [Property]
        public string separation { get; set; }

        [Property]
        public double length { get; set; }

        [Property]
        public double coordinate_x { get; set; }

        [Property]
        public double coordinate_y { get; set; }

        [Property]
        public double coordinate_z { get; set; }

        [Property]
        public string bid { get; set; }
    }
}