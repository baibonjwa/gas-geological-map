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
        public int tunnel_id { get; set; }

        /// <summary>
        ///     巷道名称
        /// </summary>
        [Property]
        public string tunnel_name { get; set; }

        /// <summary>
        ///     支护方式
        /// </summary>
        [Property]
        public string tunnel_support_pattern { get; set; }

        /// <summary>
        ///     围岩类型
        /// </summary>
        [BelongsTo("LithologyId")]
        public Lithology lithology { get; set; }

        /// <summary>
        ///     断面类型
        /// </summary>
        [Property]
        public string tunnel_section_type { get; set; }

        /// <summary>
        ///     断面参数
        /// </summary>
        [Property]
        public string tunnel_param { get; set; }

        /// <summary>
        ///     设计长度
        /// </summary>
        [Property]
        public double tunnel_design_length { get; set; }

        /// <summary>
        ///     设计面积
        /// </summary>
        [Property]
        public double tunnel_design_area { get; set; }

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property]
        public TunnelTypeEnum tunnel_type { get; set; }

        /// <summary>
        ///     工作面
        /// </summary>
        [BelongsTo("WorkingFaceId")]
        public WorkingFace working_face { get; set; }

        /// <summary>
        ///     煤巷岩巷
        /// </summary>
        [Property]
        public string coal_or_stone { get; set; }

        /// <summary>
        ///     绑定煤层ID
        /// </summary>
        //[BelongsTo("COAL_LAYER_ID")]
        public CoalSeams coal_seams { get; set; }


        public Wire wire { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }


        [Property]
        public string rule_ids { get; set; }

        [Property]
        public string pre_warning_params { get; set; }

        /// <summary>
        ///     巷道使用次数，用来缓存信息，此外无实际意义。
        /// </summary>
        public int used_times { get; set; }

        /// <summary>
        ///     巷道宽度
        /// </summary>
        [Property]
        public double tunnel_width { get; set; }

        public static Tunnel[] find_all_by_working_face_id(int workingfaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingfaceId)
            };
            return FindAll(criterion);
        }

        public static Tunnel find_first_by_working_face_id(int workingfaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingfaceId)
            };
            return FindFirst(criterion);
        }

        public static bool exists_by_tunnel_name_and_working_face_id(string tunnelName, int workingfaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("TunnelName", tunnelName),
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingfaceId)
            };
            return Exists(criterion);
        }

        public static Tunnel[] find_all_by_tunnel_type(TunnelTypeEnum tunnelType)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("TunnelType", tunnelType)
            };
            return FindAll(criterion);
        }

        public static Tunnel[] find_all_with_has_rules()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.IsNotNull("RuleIds")
            };
            return FindAll(criterion);
        }

        public override void Delete()
        {
            var wire = Wire.find_one_by_tunnel_id(tunnel_id);
            if (wire != null)
            {
                WirePoint.DeleteAll(WirePoint.find_all_by_wire_id(wire.wire_id).Select(u => u.wire_point_id));
                wire.Delete();
            }
            base.Delete();
        }
    }
}