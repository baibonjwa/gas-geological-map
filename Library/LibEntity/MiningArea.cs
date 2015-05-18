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
        public int mining_area_id { get; set; }

        /// <summary>
        ///     采区名称
        /// </summary>
        [Property]
        public string mining_area_name { get; set; }

        /// <summary>
        ///     水平
        /// </summary>
        [BelongsTo("HorizontalId")]
        public Horizontal horizontal { get; set; }

        public static MiningArea[] find_all_by_horizontal_id(int horizontalId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Horizontal.HorizontalId", horizontalId)
            };
            return FindAll(criterion);
        }

        public static MiningArea find_one_by_mining_area_name(string miningAreaName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MiningAreaName", miningAreaName)
            };
            return FindOne(criterion);
        }
    }
}