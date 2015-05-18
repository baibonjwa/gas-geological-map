using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Horizontal : ActiveRecordBase<Horizontal>
    {

        /// <summary>
        ///     水平编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int horizontal_id { get; set; }

        /// <summary>
        ///     水平名称
        /// </summary>
        [Property]
        public string horizontal_name { get; set; }

        /// <summary>
        ///     矿井
        /// </summary>
        [BelongsTo("MineId")]
        public Mine mine { get; set; }

        public static Horizontal[] find_all_by_mine_id(int mineId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Mine.MineId", mineId)
            };
            return FindAll(criterion);
        }
    }
}