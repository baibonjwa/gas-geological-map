using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NHibernate.Engine;

namespace LibEntity
{
    [ActiveRecord("T_TUNNEL_INFO")]
    public class Tunnel : ActiveRecordBase<Tunnel>
    {
        /// <summary>
        ///     巷道编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "TUNNEL_ID")]
        public int TunnelId { get; set; }

        /// <summary>
        ///     巷道名称
        /// </summary>
        [Property("TUNNEL_NAME")]
        public string TunnelName { get; set; }

        /// <summary>
        ///     支护方式
        /// </summary>
        [Property("SUPPORT_PATTERN")]
        public string TunnelSupportPattern { get; set; }

        // 围岩类型

        /// <summary>
        ///     围岩类型
        /// </summary>
        [BelongsTo("LITHOLOGY_ID")]
        public Lithology Lithology { get; set; }

        // 断面类型

        /// <summary>
        ///     断面类型
        /// </summary>
        [Property("SECTION_TYPE")]
        public string TunnelSectionType { get; set; }

        // 断面参数

        /// <summary>
        ///     断面参数
        /// </summary>
        [Property("PARAM")]
        public string TunnelParam { get; set; }

        // 设计长度

        /// <summary>
        ///     设计长度
        /// </summary>
        [Property("DESIGNLENGTH")]
        public double TunnelDesignLength { get; set; }

        // 设计面积

        /// <summary>
        ///     设计面积
        /// </summary>
        [Property("DESIGNAREA")]
        public double TunnelDesignArea { get; set; }

        //巷道类型

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property("TUNNEL_TYPE")]
        public TunnelTypeEnum TunnelType { get; set; }

        // 工作面

        /// <summary>
        ///     工作面
        /// </summary>
        [BelongsTo("WORKINGFACE_ID")]
        public WorkingFace WorkingFace { get; set; }

        // 煤巷岩巷

        /// <summary>
        ///     煤巷岩巷
        /// </summary>
        [Property("COAL_OR_STONE")]
        public string CoalOrStone { get; set; }

        // 绑定煤层ID

        /// <summary>
        ///     绑定煤层ID
        /// </summary>
        [BelongsTo("COAL_LAYER_ID")]
        public CoalSeams CoalSeams { get; set; }


        public Wire Wire { get; set; }

        // BID

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingId { get; set; }


        [Property("RULE_IDS")]
        public string RuleIds { get; set; }

        /// <summary>
        ///     巷道使用次数，用来缓存信息，此外无实际意义。
        /// </summary>
        public int UsedTimes { get; set; }

        /// <summary>
        ///     巷道宽度
        /// </summary>
        [Property("Tunnel_Wid")]
        public double TunnelWid { get; set; }

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