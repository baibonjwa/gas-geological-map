using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class InferFaultage : ActiveRecordBase<InferFaultage>
    {
        public const string TABLE_NAME = "InferFaultage";
        public const string C_FAULTAGE_NAME = "BigFaultageName";

        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [HasMany(typeof (InferFaultagePoint), Table = "InferFaultagePoints", ColumnKey = "id",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<InferFaultagePoint> infer_faultage_points { get; set; }

        [Property]
        public string name { get; set; }

        [Property]
        public string gap { get; set; }

        [Property]
        public string angle { get; set; }

        [Property]
        public string type { get; set; }

        [Property]
        public string trend { get; set; }

        [Property]
        public string bid { get; set; }
    }
}