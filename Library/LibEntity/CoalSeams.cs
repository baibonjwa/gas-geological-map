using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class CoalSeams : ActiveRecordBase<CoalSeams>
    {
        /// <summary>
        ///     煤层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int coal_seams_id { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property]
        public string coal_seams_name { get; set; }

        public static CoalSeams find_one_by_coal_seams_name(string coalSeamsName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("CoalSeamsName",coalSeamsName)
            };
            return FindOne(criterion);
        }

    }
}