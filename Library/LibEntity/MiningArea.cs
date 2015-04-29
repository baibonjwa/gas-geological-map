using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class MiningArea : ActiveRecordBase<MiningArea>
    {
        /// <summary>
        ///     采区编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int MiningAreaId { get; set; }

        /// <summary>
        ///     采区名称
        /// </summary>
        [Property]
        public string MiningAreaName { get; set; }

        /// <summary>
        ///     水平
        /// </summary>
        [BelongsTo("HorizontalId")]
        public Horizontal Horizontal { get; set; }

        public static MiningArea[] FindAllByHorizontalId(int horizontalId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Horizontal.HorizontalId", horizontalId)
            };
            return FindAll(criterion);
        }

        public static MiningArea FindOneByMiningAreaName(string miningAreaName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MiningAreaName", miningAreaName)
            };
            return FindOne(criterion);
        }
    }
}