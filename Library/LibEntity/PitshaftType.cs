using Castle.ActiveRecord;

namespace LibEntity
{
    public class PitshaftType : ActiveRecordBase<PitshaftType>
    {
        /// <summary>
        ///     井筒类型编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int PitshaftTypeId { get; set; }

        /// <summary>
        ///     井筒类型名称
        /// </summary>
        [Property]
        public string PitshaftTypeName { get; set; }
    }
}