using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("infer_faultage_points")]
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

        [BelongsTo("infer_faultage_id")]
        public InferFaultage infer_faultage { get; set; }

        [Property]
        public virtual string bid { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}