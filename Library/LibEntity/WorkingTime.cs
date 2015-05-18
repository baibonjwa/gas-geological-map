using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    /// <summary>
    ///     工作时间类
    /// </summary>
    [ActiveRecord("T_WORK_TIME")]
    public class WorkTime : ActiveRecordBase<WorkTime>
    {

        public const String TABLE_NAME = "T_WORK_TIME";
        /// <summary>
        ///     获取设置工作制Id
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "WORK_TIME_ID")]
        public string id { get; set; }

        /// <summary>
        ///     获取设置工作制制式类别
        /// </summary>
        [Property("WORK_TIME_GROUP_ID")]
        public int work_time_group_id { get; set; }

        /// <summary>
        ///     获取设置工作制名称
        /// </summary>
        [Property("WORK_TIME_NAME")]
        public string work_time_name { get; set; }

        /// <summary>
        ///     获取设置工作起始时间
        /// </summary>
        [Property("WORK_TIME_FROM")]
        public DateTime work_time_from { get; set; }

        /// <summary>
        ///     获取设置工作终止时间
        /// </summary>
        [Property("WORK_TIME_TO")]
        public DateTime work_time_to { get; set; }

        public static WorkTime[] find_all_by_work_time_group_id(int workTimeGroupId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkTimeGroupId", workTimeGroupId)
            };
            return FindAll(criterion);
        }

        public static WorkTime find_one_by_work_time_name(string workTimeName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkTimeName", workTimeName)
            };
            return FindOne(criterion);
        }


        public static WorkTime[] find_all_by38_times()
        {
            return find_all_by_work_time_group_id(1);
        }

        public static WorkTime[] find_all_by46_times()
        {
            return find_all_by_work_time_group_id(2);
        }

        public static DateTime[] get_date_shift_times(string strWorkTimeName)
        {
            var workingTime = find_one_by_work_time_name(strWorkTimeName);
            return new[] { workingTime.work_time_from, workingTime.work_time_to };
        }



    }
}