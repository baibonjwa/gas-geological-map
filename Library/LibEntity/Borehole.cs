using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Borehole : ActiveRecordBase<Borehole>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [HasMany(typeof(SubBorehole), Table = "SubBorehole", ColumnKey = "id",
Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<SubBorehole> borehole_lithologys { get; set; }

        [Property]
        public string borehole_number { get; set; }

        [Property]
        public double ground_elevation { get; set; }

        [Property]
        public double coordinate_x { get; set; }

        [Property]
        public double coordinate_y { get; set; }

        [Property]
        public double coordinate_z { get; set; }

        [Property]
        public string coal_seam_texture { get; set; }

        [Property]
        public string bid { get; set; }
    }
}