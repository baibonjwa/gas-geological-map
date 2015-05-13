using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Tunnel : ActiveRecordBase<Tunnel>
    {
        /// <summary>
        ///     巷道编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int TunnelId { get; set; }

        /// <summary>
        ///     巷道名称
        /// </summary>
        [Property]
        public string TunnelName { get; set; }

        /// <summary>
        ///     支护方式
        /// </summary>
        [Property]
        public string TunnelSupportPattern { get; set; }

        /// <summary>
        ///     围岩类型
        /// </summary>
        [BelongsTo("LithologyId")]
        public Lithology Lithology { get; set; }

        /// <summary>
        ///     断面类型
        /// </summary>
        [Property]
        public string TunnelSectionType { get; set; }

        /// <summary>
        ///     断面参数
        /// </summary>
        [Property]
        public string TunnelParam { get; set; }

        /// <summary>
        ///     设计长度
        /// </summary>
        [Property]
        public double TunnelDesignLength { get; set; }

        /// <summary>
        ///     设计面积
        /// </summary>
        [Property]
        public double TunnelDesignArea { get; set; }

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property]
        public TunnelTypeEnum TunnelType { get; set; }

        /// <summary>
        ///     工作面
        /// </summary>
        [BelongsTo("WorkingFaceId")]
        public WorkingFace WorkingFace { get; set; }

        /// <summary>
        ///     煤巷岩巷
        /// </summary>
        [Property]
        public string CoalOrStone { get; set; }

        /// <summary>
        ///     绑定煤层ID
        /// </summary>
        //[BelongsTo("COAL_LAYER_ID")]
        public CoalSeams CoalSeams { get; set; }


        public Wire Wire { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }


        [Property]
        public string RuleIds { get; set; }

        [Property]
        public string PreWarningParams { get; set; }

        /// <summary>
        ///     巷道使用次数，用来缓存信息，此外无实际意义。
        /// </summary>
        public int UsedTimes { get; set; }

        /// <summary>
        ///     巷道宽度
        /// </summary>
        [Property]
        public double TunnelWidth { get; set; }

        public static Tunnel[] FindAllByWorkingFaceId(int workingfaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingfaceId)
            };
            return FindAll(criterion);
        }

        public static Tunnel FindFirstByWorkingFaceId(int workingfaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingfaceId)
            };
            return FindFirst(criterion);
        }

        public static bool ExistsByTunnelNameAndWorkingFaceId(string tunnelName, int workingfaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("TunnelName", tunnelName),
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingfaceId)
            };
            return Exists(criterion);
        }

        public static Tunnel[] FindAllByTunnelType(TunnelTypeEnum tunnelType)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("TunnelType", tunnelType)
            };
            return FindAll(criterion);
        }

        public static Tunnel[] FindAllWithHasRules()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.IsNotNull("RuleIds")
            };
            return FindAll(criterion);
        }

        public override void Delete()
        {
            var wire = Wire.FindOneByTunnelId(TunnelId);
            if (wire != null)
            {
                WirePoint.DeleteAll(WirePoint.FindAllByWireId(wire.WireId).Select(u => u.WirePointId));
                wire.Delete();
            }
            base.Delete();
        }
    }
}