using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class WirePoint : ActiveRecordBase<WirePoint>
    {
        private Wire _wire;

        /// <summary>
        ///     构造方法
        /// </summary>
        public WirePoint()
        {
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="src">导线点实体</param>
        public WirePoint(WirePoint src)
        {
            wire_point_id = src.wire_point_id;
            coordinate_x = src.coordinate_x;
            coordinate_y = src.coordinate_y;
            coordinate_z = src.coordinate_z;
            left_dis = src.left_dis;
            right_dis = src.right_dis;
            _wire = src._wire;
        }

        /// <summary>
        ///     主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int wire_point_id { get; set; }

        [Property]
        public string wire_point_name { get; set; }

        /// <summary>
        ///     距底板距离
        /// </summary>
        [Property]
        public double bottom_dis { get; set; }

        /// <summary>
        ///     距顶板距离
        /// </summary>
        [Property]
        public double top_dis { get; set; }

        /// <summary>
        ///     绑定导线编号
        /// </summary>
        [BelongsTo("WireId")]
        public Wire wire
        {
            get { return _wire; }
            set { _wire = value; }
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property]
        public double coordinate_x { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property]
        public double coordinate_y { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property]
        public double coordinate_z { get; set; }

        /// <summary>
        ///     距左帮距离
        /// </summary>
        [Property]
        public double left_dis { get; set; }

        /// <summary>
        ///     距右帮距离
        /// </summary>
        [Property]
        public double right_dis { get; set; }

        /// <summary>
        ///     绑定ID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        public static bool exists_by_wire_point_id_in_wire_info(int wireInfoId, string wirePointId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("Wire.WireId", wireInfoId),
                Restrictions.Eq("WirePointName", wirePointId)
            };
            return Exists(criterion.ToArray());
        }


        public static WirePoint[] find_all_by_wire_id(int wireId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Wire.WireId", wireId)
            };
            return FindAll(criterion);
        }

        //public static void DeleteAllByWireId(int wireId)
        //{
        //    var criterion = new ICriterion[]
        //    {
        //        Restrictions.Eq("Wire.WireId", wireId)
        //    };
        //    DeleteAll(FindAll(criterion).Select(u => u.WirePointId));
        //}
    }
}