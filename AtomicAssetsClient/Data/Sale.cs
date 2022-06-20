using System.Text.Json.Serialization;
using AtomicAssetsClient.Utils;

namespace AtomicAssetsClient.Data
{
    public class Sale
    {
        public int SaleId { get; set; }

        public string Seller { get; set; } = string.Empty;

        public string? Buyer { get; set; }

        public int OfferId { get; set; }

        public string MarketContract { get; set; } = string.Empty;

        public string AssetsContract { get; set; } = string.Empty;

        public SalePrice? Price { get; set; }

        public decimal ListingPrice { get; set; }

        public string ListingSymbol { get; set; } = string.Empty;

        public List<Asset>? Assets { get; set; }

        public string MakerMarketplace { get; set; } = string.Empty;

        public string TakerMarketplace { get; set; } = string.Empty;

        public string CollectionName { get; set; } = string.Empty;

        public Collection? Collection { get; set; }

        public bool IsSellerContract { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset UpdatedAtTime { get; set; }

        public long UpdatedAtBlock { get; set; }

        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTimeOffset CreatedAtTime { get; set; }

        public long CreatedAtBlock { get; set; }

        public int State { get; set; }

        public class SalePrice
        {
            public string TokenContract { get; set; } = string.Empty;

            public string TokenSymbol { get; set; } = string.Empty;

            public int TokenPrecision { get; set; }

            public decimal? Median { get; set; }

            public decimal Amount { get; set; }
        }
    }
}
