using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class WorkingFace : ActiveRecordBase<WorkingFace>
    {
        /** 工作面编号 **/
        private DateTime _startDate;
        private DateTime _stopDate;

        [HasMany(typeof(Tunnel), ColumnKey = "WorkingFaceId", Table = "Tunnel",
            Cascade = ManyRelationCascadeEnum.SaveUpdate, Lazy = true)]
        public IList<Tunnel> Tunnels { get; set; }

        /// <summary>
        ///     工作面编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int WorkingFaceId { get; set; }

        /** 工作面名称 **/

        /// <summary>
        ///     工作面名称
        /// </summary>
        [Property]
        public string WorkingFaceName { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double CoordinateX { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double CoordinateY { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double CoordinateZ { get; set; }

        // 开工日期

        /// <summary>
        ///     设置或获取开工日期
        /// </summary>
        //[Property("START_DATE")]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        // 是否掘进完毕

        /// <summary>
        ///     设置或获取是否掘进完毕
        /// </summary>
        [Property]
        public int IsFinish { get; set; }

        // 停工日期

        /// <summary>
        ///     设置或获取停工日期
        /// </summary>
        //[Property("STOP_DATE")]
        public DateTime StopDate
        {
            get { return _stopDate; }
            set { _stopDate = value; }
        }


        // 采区
        [BelongsTo("MiningAreaId")]
        public MiningArea MiningArea { get; set; }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        [Property]
        public string WorkStyle { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        [Property]
        public string WorkTime { get; set; }

        // 用于暂存关联巷道信息

        public void SetCoordinate(double xx, double yy, double zz)
        {
            CoordinateX = xx;
            CoordinateY = yy;
            CoordinateZ = zz;
        }

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property]
        public WorkingfaceTypeEnum WorkingfaceType { get; set; }

        public static WorkingFace[] FindAllByMiningAreaId(int miningAreaId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MiningArea.MiningAreaId", miningAreaId)
            };
            return FindAll(criterion);
        }

        public static WorkingFace FindByWorkingFaceName(string workingFaceName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFaceName", workingFaceName)
            };
            return FindOne(criterion);
        }

        public static WorkingFace FindByWorkingFaceNameAndMiningAreaId(string workingFaceName, int miningAreaId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFaceName", workingFaceName),
                Restrictions.Eq("MiningArea.MiningAreaId",miningAreaId)
            };
            return FindOne(criterion);
        }

        public static bool ExistsByWorkingFaceName(string workingFaceName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("WorkingFaceName", workingFaceName)
            };
            return Exists(criterion.ToArray());
        }

        public static bool ExistsByMiningAreaId(int miningAreaId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("MiningArea.MiningAreaId", miningAreaId)
            };
            return Exists(criterion);
        }
    }
}