using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class InferFaultagePoint : ActiveRecordBase<InferFaultagePoint>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "id")]
        public virtual int id { get; set; }

        [Property]
        public virtual string up_or_down { get; set; }

        [Property]
        public virtual double coordinate_x { get; set; }

        [Property]
        public virtual double coordinate_y { get; set; }

        [Property]
        public virtual double coordinate_z { get; set; }

        [BelongsTo("id")]
        public InferFaultage infer_faultage { get; set; }

        [Property]
        public virtual string bid { get; set; }
    }
}