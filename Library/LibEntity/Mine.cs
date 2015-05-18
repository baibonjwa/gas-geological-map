using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Mine : ActiveRecordBase<Mine>
    {
        /// <summary>
        ///     矿井编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int mine_id { get; set; }

        /// <summary>
        ///     矿井名称
        /// </summary>
        [Property]
        public string mine_name { get; set; }

        public static bool exists_by_mine_name(string mineName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MineName", mineName)
            };
            return Exists(criterion);
        }
    }


}