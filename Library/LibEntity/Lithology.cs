using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Lithology : ActiveRecordBase<Lithology>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int lithology_id { get; set; }

        [Property]
        public string lithology_name { get; set; }

        [Property]
        public string lithology_describe { get; set; }

        public static Lithology find_one_by_coal()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LithologyName", "煤层")
            };
            return FindOne(criterion);
        }

        public static Lithology find_one_by_lithology_name(string lithologyName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LithologyName", lithologyName)
            };
            return FindOne(criterion);
        }
    }
}