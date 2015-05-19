using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Workingface : ActiveRecordBase<Workingface>
    {
        [HasMany(typeof(Tunnel), ColumnKey = "id", Table = "Tunnel",
            Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<Tunnel> tunnels { get; set; }

        /// <summary>
        ///     工作面编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        /// <summary>
        ///     工作面名称
        /// </summary>
        [Property]
        public string name { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double coordinate_x { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double coordinate_y { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double coordinate_z { get; set; }

        /// <summary>
        ///     设置或获取开工日期
        /// </summary>
        //[Property("START_DATE")]
        public DateTime start_date { get; set; }

        /// <summary>
        ///     设置或获取是否掘进完毕
        /// </summary>
        [Property]
        public int is_finish { get; set; }

        // 停工日期

        /// <summary>
        ///     设置或获取停工日期
        /// </summary>
        //[Property("STOP_DATE")]
        public DateTime stop_date { get; set; }


        // 采区
        [BelongsTo("MiningAreaId")]
        public MiningArea mining_area { get; set; }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        [Property]
        public string work_style { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        [Property]
        public string work_time { get; set; }

        // 用于暂存关联巷道信息

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
                Restrictions.Eq("WorkingFaceName", workingFaceName),
                Restrictions.Eq("MiningArea.MiningAreaId",miningAreaId)
            };
            return FindOne(criterion);
        }


        public static bool exists_by_mining_area_id(int miningAreaId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("MiningArea.MiningAreaId", miningAreaId)
            };
            return Exists(criterion);
        }
    }
}