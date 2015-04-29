using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class BigFaultagePoint : ActiveRecordBase<BigFaultagePoint>
    {

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")] 
        public virtual int Id { get; set; }

        /** 断层名称 **/
        [Property]
        public virtual string UpOrDown { get; set; }

        [Property]
        public virtual double CoordinateX { get; set; }

        [Property]
        public virtual double CoordinateY { get; set; }

        [Property]
        public virtual double CoordinateZ { get; set; }

        [BelongsTo("BigFaultageId")]
        public BigFaultage BigFaultage { get; set; }

        [Property]
        public virtual string BindingId { get; set; }

        public static BigFaultagePoint[] FindAllByFaultageId(int bigFaultageId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("BigFaultage.BigFaultageId", bigFaultageId) };
            return FindAll(criterion.ToArray());
        }
    }
}