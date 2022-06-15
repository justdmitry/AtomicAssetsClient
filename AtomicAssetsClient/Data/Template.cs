using System.Text.Json;
using System.Text.Json.Serialization;
using AtomicAssetsClient.Utils;

namespace AtomicAssetsClient.Data
{
    public class Template
    {
        public string Contract { get; set; } = string.Empty;

        public int TemplateId { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsTransferable { get; set; }

        public bool IsBurnable { get; set; }

        public int IssuedSupply { get; set; }

        public int MaxSupply { get; set; }

        public Collection? Collection { get; set; }

        public Schema? Schema { get; set; }

        public Dictionary<string, JsonElement>? ImmutableData { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset CreatedAtTime { get; set; }

        public long CreatedAtBlock { get; set; }
    }
}
