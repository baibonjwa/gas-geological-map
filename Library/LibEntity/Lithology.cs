using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class Lithology : ActiveRecordBase<Lithology>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int LithologyId { get; set; }

        [Property]
        public string LithologyName { get; set; }

        [Property]
        public string LithologyDescribe { get; set; }

        public static Lithology FindOneByCoal()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LithologyName", "煤层")
            };
            return FindOne(criterion);
        }

        public static Lithology FindOneByLithologyName(string lithologyName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LithologyName", lithologyName)
            };
            return FindOne(criterion);
        }
    }
}