using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    public class Pitshaft : ActiveRecordBase<Pitshaft>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public string pitshaft_name { get; set; }

        [Property]
        public string pitshaft_type { get; set; }

        [Property]
        public double wellhead_elevation { get; set; }

        [Property]
        public double wellbottom_elevation { get; set; }

        [Property]
        public double pitshaft_coordinate_x { get; set; }

        [Property]
        public double pitshaft_coordinate_y { get; set; }

        [Property]
        public double figure_coordinate_x { get; set; }

        [Property]
        public double figure_coordinate_y { get; set; }

        [Property]
        public double figure_coordinate_z { get; set; }

        [Property]
        public string bid { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}