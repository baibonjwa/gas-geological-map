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
        public int prospecting_line_id { get; set; }

        /// <summary>
        ///     勘探线名称
        /// </summary>
        [Property]
        public string prospecting_line_name { get; set; }

        /// <summary>
        ///     勘探钻孔
        /// </summary>
        [Property]
        public string prospecting_borehole { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property]
        public string binding_id { get; set; }

        public static bool exists_by_prospecting_line_name(string prospectingLineName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("ProspectingLineName", prospectingLineName)
            };
            return Exists(criterion.ToArray());
        }
    }
}