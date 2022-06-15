using System.Text.Json;
using System.Text.Json.Serialization;
using AtomicAssetsClient.Utils;

namespace AtomicAssetsClient.Data
{
    public class Asset
    {
        public string Contract { get; set; } = string.Empty;

        public long AssetId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Owner { get; set; } = string.Empty;

        public bool IsTransferable { get; set; }

        public bool IsBurnable { get; set; }

        public Collection? Collection { get; set; }

        public Schema? Schema { get; set; }

        public Template? Template { get; set; }

        public int TemplateMint { get; set; }

        public Dictionary<string, JsonElement>? MutableData { get; set; }

        public Dictionary<string, JsonElement>? ImmutableData { get; set; }

        public Dictionary<string, JsonElement>? Data { get; set; }

        public string? BurnedByAccount { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset? BurnedAtTime { get; set; }

        public long? BurnedAtBlock { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset UpdatedAtTime { get; set; }

        public long UpdatedAtBlock { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset TransferredAtTime { get; set; }

        public long TransferredAtBlock { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset MintedAtTime { get; set; }

        public long MintedAtBlock { get; set; }
    }
}
