using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("workingfaces")]
    public class Workingface : ActiveRecordBase<Workingface>
    {
        [HasMany(typeof(Tunnel), ColumnKey = "workingface_id", Table = "tunnels",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<Tunnel> tunnels { get; set; }

        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [Property]
        public double coordinate_x { get; set; }

        [Property]
        public double coordinate_y { get; set; }

        [Property]
        public double coordinate_z { get; set; }

        [Property]
        public DateTime start_date { get; set; }

        [Property]
        public int is_finish { get; set; }

        [Property]
        public DateTime stop_date { get; set; }

        [BelongsTo("mining_area_id")]
        public MiningArea mining_area { get; set; }

        [Property]
        public string work_style { get; set; }

        [Property]
        public string work_time { get; set; }


        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;

        public void set_coordinate(double xx, double yy, double zz)
        {
            coordinate_x = xx;
            coordinate_y = yy;
            coordinate_z = zz;
        }

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property]
        public WorkingfaceTypeEnum type { get; set; }

        public static Workingface findone_by_workingface_name_and_mining_area_id(string workingFaceName, int miningAreaId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("name", workingFaceName),
                Restrictions.Eq("mining_area.id",miningAreaId)
            };
            return FindOne(criterion);
        }
    }
}