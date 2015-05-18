using Castle.ActiveRecord;

namespace LibEntity
{
    public class PitshaftType : ActiveRecordBase<PitshaftType>
    {
        /// <summary>
        ///     井筒类型编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int pitshaft_type_id { get; set; }

        /// <summary>
        ///     井筒类型名称
        /// </summary>
        [Property]
        public string pitshaft_type_name { get; set; }
    }
}