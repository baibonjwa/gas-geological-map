using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord]
    public class MiningArea : ActiveRecordBase<MiningArea>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [BelongsTo("id")]
        public Horizontal horizontal { get; set; }
    }
}