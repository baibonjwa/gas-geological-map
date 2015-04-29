using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord]
    public class ProspectingLine : ActiveRecordBase<ProspectingLine>
    {
        /// <summary>
        ///     勘探线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int ProspectingLineId { get; set; }

        /// <summary>
        ///     勘探线名称
        /// </summary>
        [Property]
        public string ProspectingLineName { get; set; }

        /// <summary>
        ///     勘探钻孔
        /// </summary>
        [Property]
        public string ProspectingBorehole { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string BindingId { get; set; }

        public static bool ExistsByProspectingLineName(string prospectingLineName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("ProspectingLineName", prospectingLineName)
            };
            return Exists(criterion.ToArray());
        }
    }
}