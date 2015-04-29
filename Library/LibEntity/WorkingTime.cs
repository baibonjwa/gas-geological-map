using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    /// <summary>
    ///     工作时间类
    /// </summary>
    [ActiveRecord("T_WORK_TIME")]
    public class WorkingTime : ActiveRecordBase<WorkingTime>
    {

        public const String TableName = "T_WORK_TIME";
        /// <summary>
        ///     获取设置工作制Id
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "WORK_TIME_ID")]
        public string Id { get; set; }

        /// <summary>
        ///     获取设置工作制制式类别
        /// </summary>
        [Property("WORK_TIME_GROUP_ID")]
        public int WorkTimeGroupId { get; set; }

        /// <summary>
        ///     获取设置工作制名称
        /// </summary>
        [Property("WORK_TIME_NAME")]
        public string WorkTimeName { get; set; }

        /// <summary>
        ///     获取设置工作起始时间
        /// </summary>
        [Property("WORK_TIME_FROM")]
        public DateTime WorkTimeFrom { get; set; }

        /// <summary>
        ///     获取设置工作终止时间
        /// </summary>
        [Property("WORK_TIME_TO")]
        public DateTime WorkTimeTo { get; set; }

        public static WorkingTime[] FindAllByWorkTimeGroupId(int workTimeGroupId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkTimeGroupId", workTimeGroupId)
            };
            return FindAll(criterion);
        }

        public static WorkingTime FindOneByWorkTimeName(string workTimeName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkTimeName", workTimeName)
            };
            return FindOne(criterion);
        }


        public static WorkingTime[] FindAllBy38Times()
        {
            return FindAllByWorkTimeGroupId(1);
        }

        public static WorkingTime[] FindAllBy46Times()
        {
            return FindAllByWorkTimeGroupId(2);
        }

        public static DateTime[] GetDateShiftTimes(string strWorkTimeName)
        {
            var workingTime = FindOneByWorkTimeName(strWorkTimeName);
            return new[] { workingTime.WorkTimeFrom, workingTime.WorkTimeTo };
        }



    }
}