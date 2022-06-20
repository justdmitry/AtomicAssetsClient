namespace AtomicAssetsClient.Data
{
    public class SaleDeserialization
    {
        [Fact]
        public void Test()
        {
            var opts = new AtomicClientOptions();
            var obj = System.Text.Json.JsonSerializer.Deserialize<AtomicResponse<Sale>>(Samples.GetSale, opts.JsonOptions);

            Assert.NotNull(obj);
            Assert.True(obj.Success);

            var item = obj.Data;
            Assert.NotNull(item);

            Assert.Equal("atomicmarket", item.MarketContract);
            Assert.Equal("atomicassets", item.AssetsContract);
            Assert.Equal(123456, item.SaleId);
            Assert.Equal("jnxau.wam", item.Seller);
            Assert.Equal("test-test", item.Buyer);
            Assert.Equal(125012, item.OfferId);

            Assert.NotNull(item.Price);
            Assert.Equal("eosio.token", item.Price.TokenContract);
            Assert.Equal("WAX", item.Price.TokenSymbol);
            Assert.Equal(8, item.Price.TokenPrecision);
            Assert.Null(item.Price.Median);
            Assert.Equal(60000000000, item.Price.Amount);

            Assert.Equal(60000000000, item.ListingPrice);
            Assert.Equal("WAX", item.ListingSymbol);

            Assert.NotNull(item.Assets);
            Assert.Single(item.Assets);
            Assert.Equal(1099511859709, item.Assets[0].AssetId);

            Assert.Equal("test1", item.MakerMarketplace);
            Assert.Equal("test2", item.TakerMarketplace);
            Assert.Equal("kogsofficial", item.CollectionName);

            Assert.NotNull(item.Collection);
            Assert.Equal("kogsofficial", item.Collection.CollectionName);

            Assert.False(item.IsSellerContract);

            Assert.Equal(new DateTimeOffset(2020, 8, 19, 10, 33, 35, TimeSpan.Zero), item.UpdatedAtTime);
            Assert.Equal(72804734, item.UpdatedAtBlock);

            Assert.Equal(new DateTimeOffset(2020, 8, 19, 5, 25, 12, TimeSpan.Zero), item.CreatedAtTime);
            Assert.Equal(72767728, item.CreatedAtBlock);

            Assert.Equal(SaleState.Cancelled, item.State);
        }
    }
}
