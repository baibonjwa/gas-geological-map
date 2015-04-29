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
        public int HorizontalId { get; set; }

        /// <summary>
        ///     水平名称
        /// </summary>
        [Property]
        public string HorizontalName { get; set; }

        /// <summary>
        ///     矿井
        /// </summary>
        [BelongsTo("MineId")]
        public Mine Mine { get; set; }

        public static Horizontal[] FindAllByMineId(int mineId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Mine.MineId", mineId)
            };
            return FindAll(criterion);
        }
    }
}