using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("boreholes")]
    public class Borehole : ActiveRecordBase<Borehole>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [HasMany(typeof(SubBorehole), Table = "sub_boreholes", ColumnKey = "borehole_id",
Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<SubBorehole> sub_boreholes { get; set; }

        [Property]
        public string name { get; set; }

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

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;


    }
}