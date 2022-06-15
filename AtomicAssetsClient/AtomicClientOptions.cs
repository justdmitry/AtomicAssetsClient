namespace AtomicAssetsClient
{
    using System;
    using System.Text.Json;

    public class AtomicClientOptions
    {
        public Uri Endpoint { get; set; } = new Uri("https://wax.api.atomicassets.io/");

        public int PageSize { get; set; } = 100;

        public JsonSerializerOptions JsonOptions { get; set; } = new JsonSerializerOptions
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = Utils.SnakeCaseNamingPolicy.Instance,
        };
    }
}
