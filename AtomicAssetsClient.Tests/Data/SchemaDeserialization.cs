namespace AtomicAssetsClient.Data
{
    public class SchemaDeserialization
    {
        [Fact]
        public void Test()
        {
            var opts = new AtomicClientOptions();
            var obj = System.Text.Json.JsonSerializer.Deserialize<AtomicResponse<Schema>>(Samples.GetSchema, opts.JsonOptions);

            Assert.NotNull(obj);
            Assert.True(obj.Success);

            var item = obj.Data;
            Assert.NotNull(item);

            Assert.Equal("atomicassets", item.Contract);
            Assert.Equal("awards", item.SchemaName);

            Assert.NotNull(item.Collection);
            Assert.Equal("atomic", item.Collection.CollectionName);

            Assert.Equal(new DateTimeOffset(2020, 6, 30, 1, 55, 57, TimeSpan.Zero), item.CreatedAtTime);
            Assert.Equal(64105836, item.CreatedAtBlock);
        }
    }
}
