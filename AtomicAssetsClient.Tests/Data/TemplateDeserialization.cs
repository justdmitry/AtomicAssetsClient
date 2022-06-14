namespace AtomicAssetsClient.Data
{
    public class TemplateDeserialization
    {
        [Fact]
        public void Test()
        {
            var opts = new AtomicClientOptions();
            var obj = System.Text.Json.JsonSerializer.Deserialize<AtomicResponse<Template>>(Samples.GetTemplate, opts.JsonOptions);

            Assert.NotNull(obj);
            Assert.True(obj.Success);

            var item = obj.Data;
            Assert.NotNull(item);

            Assert.Equal("atomicassets", item.Contract);
            Assert.Equal(1, item.TemplateId);
            Assert.Equal("Atomic Beta Award", item.Name);
            Assert.True(item.IsTransferable);
            Assert.True(item.IsBurnable);
            Assert.Equal(154, item.IssuedSupply);
            Assert.Equal(1540, item.MaxSupply);

            Assert.NotNull(item.Collection);
            Assert.Equal("atomic", item.Collection.CollectionName);

            Assert.NotNull(item.Schema);
            Assert.Equal("awards", item.Schema.SchemaName);

            Assert.Equal(new DateTimeOffset(2020, 6, 30, 1, 57, 19, 500, TimeSpan.Zero), item.CreatedAtTime);
            Assert.Equal(64106001, item.CreatedAtBlock);

            Assert.NotNull(item.ImmutableData);
            Assert.Equal("QmdBsZtZDWdeqMuRnAXtHnD4qKcnSYvhqLhmug28rEcpkS", item.ImmutableData["img"]);
            Assert.Equal("Atomic Beta Award", item.ImmutableData["name"]);
            Assert.Equal("This award was issued to those that joined the AtomicAssets Telegram channel before the official launch on the 30.06.2020", item.ImmutableData["description"]);
        }
    }
}
