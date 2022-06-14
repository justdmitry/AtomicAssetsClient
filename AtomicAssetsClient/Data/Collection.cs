using System.Text.Json.Serialization;
using AtomicAssetsClient.Utils;

namespace AtomicAssetsClient.Data
{
    public class Collection
    {
        public string Contract { get; set; } = string.Empty;

        public string CollectionName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public bool AllowNotify { get; set; }

        public string[]? AuthorizedAccounts { get; set; }

        public string[]? NotifyAccounts { get; set; }

        public decimal MarketFee { get; set; }

        public Dictionary<string, object>? Data { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset CreatedAtTime { get; set; }

        public long CreatedAtBlock { get; set; }
    }
}
