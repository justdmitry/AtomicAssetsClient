using System.Text.Json.Serialization;
using AtomicAssetsClient.Utils;

namespace AtomicAssetsClient.Data
{
    public class Schema
    {
        public string Contract { get; set; } = string.Empty;

        public string SchemaName { get; set; } = string.Empty;

        public Collection Collection { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset CreatedAtTime { get; set; }

        public long CreatedAtBlock { get; set; }
    }
}
