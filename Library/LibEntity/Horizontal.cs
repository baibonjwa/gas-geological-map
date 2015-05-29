using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("horizontals")]
    public class Horizontal : ActiveRecordBase<Horizontal>
    {

        /// <summary>
        ///     水平编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        /// <summary>
        ///     水平名称
        /// </summary>
        [Property]
        public string name { get; set; }

        /// <summary>
        ///     矿井
        /// </summary>
        [BelongsTo("id")]
        public Mine mine { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}