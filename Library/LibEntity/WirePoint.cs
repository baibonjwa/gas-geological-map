using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_WIRE_POINT")]
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
            WirePointId = src.WirePointId;
            CoordinateX = src.CoordinateX;
            CoordinateY = src.CoordinateY;
            CoordinateZ = src.CoordinateZ;
            LeftDis = src.LeftDis;
            RightDis = src.RightDis;
            _wire = src._wire;
        }

        /// <summary>
        ///     主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int WirePointId { get; set; }

        [Property("WIRE_POINT_NAME")]
        public string WirePointName { get; set; }

        /// <summary>
        ///     距底板距离
        /// </summary>
        [Property("DISTANCE_FROM_BOTTOM")]
        public double BottomDis { get; set; }

        /// <summary>
        ///     距顶板距离
        /// </summary>
        [Property("DISTANCE_FROM_TOP")]
        public double TopDis { get; set; }

        /// <summary>
        ///     绑定导线编号
        /// </summary>
        [BelongsTo("WIRE_INFO_ID")]
        public Wire Wire
        {
            get { return _wire; }
            set { _wire = value; }
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        /// <summary>
        ///     距左帮距离
        /// </summary>
        [Property("DISTANCE_FROM_THE_LEFT")]
        public double LeftDis { get; set; }

        /// <summary>
        ///     距右帮距离
        /// </summary>
        [Property("DISTANCE_FROM_THE_RIGHT")]
        public double RightDis { get; set; }

        /// <summary>
        ///     绑定ID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingId { get; set; }

        public static bool ExistsByWirePointIdInWireInfo(int wireInfoId, string wirePointId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("Wire.WireId", wireInfoId),
                Restrictions.Eq("WirePointName", wirePointId)
            };
            return Exists(criterion.ToArray());
        }


        public static WirePoint[] FindAllByWireId(int wireId)
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