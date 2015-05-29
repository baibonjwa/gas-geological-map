using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("mining_areas")]
    public class MiningArea : ActiveRecordBase<MiningArea>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [BelongsTo("horizontal_id")]
        public Horizontal horizontal { get; set; }

        [Property]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Property]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}