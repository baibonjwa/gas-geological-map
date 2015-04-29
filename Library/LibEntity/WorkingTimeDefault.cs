using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    /// <summary>
    ///     工作时间类
    /// </summary>
    [ActiveRecord("T_WORK_TIME_DEFAULT")]
    public class WorkingTimeDefault : ActiveRecordBase<WorkingTimeDefault>
    {

        public const String TableName = "T_WORK_TIME_DEFAULT";
        /// <summary>
        ///     获取设置工作制Id
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "DEFAULT_WORK_TIME_GROUP_ID")]
        public int DefaultWorkTimeGroupId { get; set; }
    }
}