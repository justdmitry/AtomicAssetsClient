namespace AtomicAssetsClient.Data
{
    public class CollectionDeserialization
    {
        [Fact]
        public void Test()
        {
            var opts = new ClientOptions();
            var obj = System.Text.Json.JsonSerializer.Deserialize<AtomicResponse<Collection>>(Samples.GetCollection, opts.JsonOptions);

            Assert.NotNull(obj);
            Assert.True(obj.Success);

            var item = obj.Data;
            Assert.NotNull(item);

            Assert.Equal("atomicassets", item.Contract);
            Assert.Equal("atomic", item.CollectionName);
            Assert.Equal("Official AtomicAssets", item.Name);
            Assert.Equal("atomic", item.Author);
            Assert.True(item.AllowNotify);
            Assert.Equal(new[] { "atomic" }, item.AuthorizedAccounts);
            Assert.Equal(new[] { "test1", "test2" }, item.NotifyAccounts);
            Assert.Equal(0.12M, item.MarketFee);
            Assert.Equal(new DateTimeOffset(2020, 6, 30, 1, 55, 54, 500, TimeSpan.Zero), item.CreatedAtTime);
            Assert.Equal(64105831, item.CreatedAtBlock);

            Assert.NotNull(item.Data);
            Assert.Equal("QmYeWdTRTEePZasagQaT4UkuRbfe1sPvyw47PZrroba9tH", item.Data["img"]);
            Assert.Equal("https://atomicassets.io/", item.Data["url"]);
            Assert.Equal("Official AtomicAssets", item.Data["name"]);
            Assert.Equal("Assets created by the AtomicAssets developers", item.Data["description"]);
        }
    }
}
