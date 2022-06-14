namespace AtomicAssetsClient.Data
{
    public class AssetDeserialization
    {
        [Fact]
        public void Test()
        {
            var opts = new AtomicClientOptions();
            var obj = System.Text.Json.JsonSerializer.Deserialize<AtomicResponse<Asset>>(Samples.GetAsset, opts.JsonOptions);

            Assert.NotNull(obj);
            Assert.True(obj.Success);

            var item = obj.Data;
            Assert.NotNull(item);

            Assert.Equal("atomicassets", item.Contract);
            Assert.Equal(1099511627776, item.AssetId);
            Assert.Equal("Atomic Beta Award", item.Name);
            Assert.Equal("intqi.wam", item.Owner);
            Assert.True(item.IsTransferable);
            Assert.True(item.IsBurnable);

            Assert.NotNull(item.Collection);
            Assert.Equal("atomic", item.Collection.CollectionName);

            Assert.NotNull(item.Schema);
            Assert.Equal("awards", item.Schema.SchemaName);

            Assert.NotNull(item.Template);
            Assert.Equal(1, item.Template.TemplateId);

            Assert.NotNull(item.ImmutableData);
            Assert.Equal("1", item.ImmutableData["mint"]);

            Assert.Equal(1, item.TemplateMint);

            Assert.Null(item.BurnedByAccount);
            Assert.Null(item.BurnedAtTime);
            Assert.Null(item.BurnedAtBlock);

            Assert.Equal(new DateTimeOffset(2020, 11, 16, 0, 20, 20, TimeSpan.Zero), item.UpdatedAtTime);
            Assert.Equal(88106238, item.UpdatedAtBlock);

            Assert.Equal(new DateTimeOffset(2020, 11, 16, 0, 20, 20, TimeSpan.Zero), item.TransferredAtTime);
            Assert.Equal(88106238, item.TransferredAtBlock);

            Assert.Equal(new DateTimeOffset(2020, 6, 30, 1, 57, 20, 500, TimeSpan.Zero), item.MintedAtTime);
            Assert.Equal(64106003, item.MintedAtBlock);
        }
    }
}
