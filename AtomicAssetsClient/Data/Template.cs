using System.Text.Json.Serialization;
using AtomicAssetsClient.Utils;

namespace AtomicAssetsClient.Data
{
    public class Template
    {
        public Schema Schema { get; set; }

        //public AssetData immutable_data { get; set; }

        public int IssuedSupply { get; set; }

        public int MaxSupply { get; set; }

        public int TemplateId { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset CreatedAtTime { get; set; }

        public long CreatedAtBlock { get; set; }
    }
}
