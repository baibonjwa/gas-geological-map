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
        public int MineId { get; set; }

        /// <summary>
        ///     矿井名称
        /// </summary>
        [Property]
        public string MineName { get; set; }

        public static bool ExistsByMineName(string mineName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MineName", mineName)
            };
            return Exists(criterion);
        }
    }


}