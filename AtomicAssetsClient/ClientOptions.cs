namespace AtomicAssetsClient
{
    using System;
    using System.Text.Json;

    public class ClientOptions
    {
        public Uri Endpoint { get; set; } = new Uri("https://wax.api.atomicassets.io/");

        public JsonSerializerOptions JsonOptions { get; set; } = new JsonSerializerOptions
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = Utils.SnakeCaseNamingPolicy.Instance,
        };
    }
}
