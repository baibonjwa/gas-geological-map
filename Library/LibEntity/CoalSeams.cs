using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    public class CoalSeams : ActiveRecordBase<CoalSeams>
    {
        /// <summary>
        ///     煤层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int CoalSeamsId { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property]
        public string CoalSeamsName { get; set; }

        public static CoalSeams FindOneByCoalSeamsName(string coalSeamsName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("CoalSeamsName",coalSeamsName)
            };
            return FindOne(criterion);
        }

    }
}