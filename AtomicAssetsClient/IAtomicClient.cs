using AtomicAssetsClient.Data;

namespace AtomicAssetsClient
{
    public interface IAtomicClient
    {
        /// <summary>
        /// Fetch asset by id. Throws exception if not found.
        /// </summary>
        /// <param name="assetId">ID of asset</param>
        /// <returns><see cref="Asset"/> instance.</returns>
        /// <remarks>Use <see cref="IAtomicClientExtensions.GetAssetOrDefault"/> to check for asset existence.</remarks>
        Task<Asset> GetAsset(long assetId);

        /// <summary>
        /// Fetch assets.
        /// </summary>
        /// <param name="collectionName">Filter by collection name.</param>
        /// <param name="schemaName">Filter by schema name.</param>
        /// <param name="templateId">Filter by template ID.</param>
        /// <param name="burned">Filter for burned assets.</param>
        /// <param name="owner">Filter by owner.</param>
        /// <param name="match">Search for input in asset name on template data.</param>
        /// <param name="search">Fuzzy search for input in asset name on template data.</param>
        /// <param name="matchImmutableName">Search for input in asset name on asset immutable data.</param>
        /// <param name="matchMutableName">Search for input in asset name on asset mutable data.</param>
        /// <param name="isTransferable">Check if asset is transferable.</param>
        /// <param name="isBurnable">Check if asset is burnable.</param>
        /// <param name="collectionBlacklist">Hide collections from result.</param>
        /// <param name="collectionWhitelist">Show only results from specific collections.</param>
        /// <param name="onlyDuplicateTemplates">Show only duplicate assets grouped by template.</param>
        /// <param name="hasBackedTokens">Show only assets that are backed by a token.</param>
        /// <param name="authorizedAccount">Filter for assets the provided account can edit.</param>
        /// <param name="templateWhitelist">Include only specific template IDs.</param>
        /// <param name="templateBlacklist">Dont include specific template IDs.</param>
        /// <param name="hideTemplatesByAccounts">Dont templates that are owned by an account.</param>
        /// <param name="hideOffers">Hide assets which are used in an offer.</param>
        /// <param name="ids">Show only results with matched ids (names).</param>
        /// <param name="lowerBound">Lower bound of primary key (value included).</param>
        /// <param name="upperBound">Upper bound of primary key (value excluded).</param>
        /// <param name="before">Only show results before this timestamp (value excluded).</param>
        /// <param name="after">Only show results after this timestamp (value excluded).</param>
        /// <param name="order">Order direction. Available values: "asc", "desc".</param>
        /// <param name="sort">Column to sort. Available values: "asset_id", "minted", "updated", "transferred", "template_mint", "name".</param>
        /// <param name="maxPages">Maxmimum number of pages (of size <see cref="AtomicClientOptions.PageSize" /> to return. Zero (0) means "all".</param>
        Task<List<Asset>> GetAssets(
            string? collectionName = null,
            string? schemaName = null,
            int? templateId = null,
            bool? burned = null,
            string? owner = null,
            string? match = null,
            string? search = null,
            string? matchImmutableName = null,
            string? matchMutableName = null,
            bool? isTransferable = null,
            bool? isBurnable = null,
            ICollection<string>? collectionBlacklist = null,
            ICollection<string>? collectionWhitelist = null,
            bool? onlyDuplicateTemplates = null,
            bool? hasBackedTokens = null,
            string? authorizedAccount = null,
            ICollection<int>? templateWhitelist = null,
            ICollection<int>? templateBlacklist = null,
            ICollection<string>? hideTemplatesByAccounts = null,
            bool? hideOffers = null,
            ICollection<long>? ids = null,
            long? lowerBound = null,
            long? upperBound = null,
            DateTimeOffset? before = null,
            DateTimeOffset? after = null,
            string? order = "desc",
            string? sort = "asset_id",
            int maxPages = 0);

        /// <summary>
        /// Find collection by its name. Throws exception if not found.
        /// </summary>
        /// <param name="collectionName">Name of collection.</param>
        /// <returns><see cref="Collection"/> instance.</returns>
        /// <remarks>Use <see cref="IAtomicClientExtensions.GetCollectionOrDefault"/> to check for collection existence.</remarks>
        Task<Collection> GetCollection(string collectionName);

        /// <summary>
        /// Fetch collections.
        /// </summary>
        /// <param name="author">Get collections by author.</param>
        /// <param name="match">Search for input in collection name.</param>
        /// <param name="authorizedAccount">Filter for collections which the provided account can use to create assets.</param>
        /// <param name="notifyAccount">Filter for collections where the provided account is notified.</param>
        /// <param name="collectionBlacklist">Hide collections from result.</param>
        /// <param name="collectionWhitelist">Show only results from specific collections.</param>
        /// <param name="ids">Show only results with matched ids (names).</param>
        /// <param name="lowerBound">Lower bound of primary key (id, name) (value included).</param>
        /// <param name="upperBound">Upper bound of primary key (id, name) (value excluded).</param>
        /// <param name="before">Only show results before this timestamp (value excluded).</param>
        /// <param name="after">Only show results after this timestamp (value excluded).</param>
        /// <param name="order">Order direction. Available values: "asc", "desc".</param>
        /// <param name="sort">Column to sort. Available values: "created", "collection_name".</param>
        /// <param name="maxPages">Maxmimum number of pages (of size <see cref="AtomicClientOptions.PageSize" /> to return. Zero (0) means "all".</param>
        /// <returns>List of found <see cref="Collection"/> instances.</returns>
        Task<List<Collection>> GetCollections(
            string? author = null,
            string? match = null,
            string? authorizedAccount = null,
            string? notifyAccount = null,
            ICollection<string>? collectionBlacklist = null,
            ICollection<string>? collectionWhitelist = null,
            ICollection<string>? ids = null,
            string? lowerBound = null,
            string? upperBound = null,
            DateTimeOffset? before = null,
            DateTimeOffset? after = null,
            string? order = "asc",
            string? sort = "created",
            int maxPages = 0);

        /// <summary>
        /// Get stats about collection.
        /// </summary>
        /// <param name="collectionName">Name of collection.</param>
        /// <returns><see cref="CollectionStats"/> instance.</returns>
        Task<CollectionStats> GetCollectionStats(string collectionName);

        /// <summary>
        /// Find schema by schema name and collection name. Throws exception if not found.
        /// </summary>
        /// <param name="collectionName">Collection name of schema.</param>
        /// <param name="schemaName">Name of schema.</param>
        /// <returns><see cref="Schema"/> instance.</returns>
        /// <remarks>Use <see cref="IAtomicClientExtensions.GetSchemaOrDefault"/> to check for schema existence.</remarks>
        Task<Schema> GetSchema(string collectionName, string schemaName);

        /// <summary>
        /// Fetch schemas.
        /// </summary>
        /// <param name="collectionName">Get all schemas within the collection.</param>
        /// <param name="authorizedAccount">Filter for schemas the provided account can edit.</param>
        /// <param name="schemaName">Schema name.</param>
        /// <param name="match">Search for input in schema name.</param>
        /// <param name="collectionBlacklist">Hide collections from result.</param>
        /// <param name="collectionWhitelist">Show only results from specific collections.</param>
        /// <param name="ids">Show only results with matched ids (names).</param>
        /// <param name="lowerBound">Lower bound of primary key (id, name) (value included).</param>
        /// <param name="upperBound">Upper bound of primary key (id, name) (value excluded).</param>
        /// <param name="before">Only show results before this timestamp (value excluded).</param>
        /// <param name="after">Only show results after this timestamp (value excluded).</param>
        /// <param name="order">Order direction. Available values: "asc", "desc".</param>
        /// <param name="sort">Column to sort. Available values: "created", "schema_name".</param>
        /// <param name="maxPages">Maxmimum number of pages (of size <see cref="AtomicClientOptions.PageSize" /> to return. Zero (0) means "all".</param>
        /// <returns>List of found <see cref="Collection"/> instances.</returns>
        Task<List<Schema>> GetSchemas(
            string? collectionName = null,
            string? authorizedAccount = null,
            string? schemaName = null,
            string? match = null,
            ICollection<string>? collectionBlacklist = null,
            ICollection<string>? collectionWhitelist = null,
            ICollection<string>? ids = null,
            string? lowerBound = null,
            string? upperBound = null,
            DateTimeOffset? before = null,
            DateTimeOffset? after = null,
            string? order = "asc",
            string? sort = "created",
            int maxPages = 0);

        /// <summary>
        /// Get stats about a specific schema.
        /// </summary>
        /// <param name="collectionName">Collection name of schema.</param>
        /// <param name="schemaName">Name of schema.</param>
        /// <returns><see cref="SchemaStats"/> instance.</returns>
        Task<SchemaStats> GetSchemaStats(string collectionName, string schemaName);

        /// <summary>
        /// Find template by id. Throws exception if not found.
        /// </summary>
        /// <param name="collectionName">Name of collection.</param>
        /// <param name="templateId">ID of template.</param>
        /// <returns><see cref="Template"/> instance.</returns>
        /// <remarks>Use <see cref="IAtomicClientExtensions.GetTemplateOrDefault"/> to check for template existence.</remarks>
        Task<Template> GetTemplate(string collectionName, int templateId);

        /// <summary>
        /// Fetch templates.
        /// </summary>
        /// <param name="collectionName">Get all templates within the collection.</param>
        /// <param name="schemaName">Get all templates which implement that schema.</param>
        /// <param name="issuedSupply">Filter by issued supply.</param>
        /// <param name="minIssuedSupply">Filter by issued supply.</param>
        /// <param name="maxIssuedSupply">Filter by issued supply.</param>
        /// <param name="hasAssets">Only show templates with existing supply &gt; 0.</param>
        /// <param name="maxSupply">Filter by max supply.</param>
        /// <param name="isBurnable">Filter by burnable.</param>
        /// <param name="isTransferable">Filter by transferable.</param>
        /// <param name="authorizedAccount">Filter for templates the provided account can use.</param>
        /// <param name="match">Search for input in template name.</param>
        /// <param name="collectionBlacklist">Hide collections from result.</param>
        /// <param name="collectionWhitelist">Show only results from specific collections.</param>
        /// <param name="ids">Show only results with matched ids (names).</param>
        /// <param name="lowerBound">Lower bound of primary key (value included).</param>
        /// <param name="upperBound">Upper bound of primary key (value excluded).</param>
        /// <param name="before">Only show results before this timestamp (value excluded).</param>
        /// <param name="after">Only show results after this timestamp (value excluded).</param>
        /// <param name="order">Order direction. Available values: "asc", "desc".</param>
        /// <param name="sort">Column to sort. Available values: "created", "name".</param>
        /// <param name="maxPages">Maxmimum number of pages (of size <see cref="AtomicClientOptions.PageSize" /> to return. Zero (0) means "all".</param>
        /// <returns>List of found <see cref="Template"/> instances.</returns>
        Task<List<Template>> GetTemplates(
            string? collectionName = null,
            string? schemaName = null,
            long? issuedSupply = null,
            long? minIssuedSupply = null,
            long? maxIssuedSupply = null,
            bool? hasAssets = null,
            long? maxSupply = null,
            bool? isBurnable = null,
            bool? isTransferable = null,
            string? authorizedAccount = null,
            string? match = null,
            ICollection<string>? collectionBlacklist = null,
            ICollection<string>? collectionWhitelist = null,
            ICollection<int>? ids = null,
            int? lowerBound = null,
            int? upperBound = null,
            DateTimeOffset? before = null,
            DateTimeOffset? after = null,
            string? order = "asc",
            string? sort = "created",
            int maxPages = 0);

        /// <summary>
        /// Get stats about a specific template.
        /// </summary>
        /// <param name="collectionName">Name of collection.</param>
        /// <param name="templateId">ID of template.</param>
        /// <returns><see cref="TemplateStats"/> instance.</returns>
        Task<TemplateStats> GetTemplateStats(string collectionName, int templateId);

        /// <summary>
        /// Get a specific sale by id. Throws exception if not found.
        /// </summary>
        /// <param name="saleId">Sale ID.</param>
        /// <returns><see cref="Sale"/> instance.</returns>
        Task<Sale> GetSale(int saleId);

        /// <summary>
        /// Get all sales.
        /// </summary>
        /// <param name="state">Filter by sale state (0: WAITING - Sale created but offer was not send yet, 1: LISTED - Assets for sale, 2: CANCELED - Sale was canceled, 3: SOLD - Sale was bought4: INVALID - Sale is still listed but offer is currently invalid (can become valid again if the user owns all assets again)).</param>
        /// <param name="maxAssets">Max assets per listing.</param>
        /// <param name="minAssets">Min assets per listing.</param>
        /// <param name="showSellerContract">If false no seller contracts are shown except if they are in the contract whitelist.</param>
        /// <param name="contractWhitelist">Show these accounts even if they are contracts.</param>
        /// <param name="sellerBlacklist">Dont show listings from these sellers.</param>
        /// <param name="buyerBlacklist">Dont show listings from these buyers.</param>
        /// <param name="assetId">Asset ID in the offer.</param>
        /// <param name="marketplace">Filter by all sales where a certain marketplace is either taker or maker marketplace.</param>
        /// <param name="makerMarketplace">Maker marketplace.</param>
        /// <param name="takerMarketplace">Taker marketplace.</param>
        /// <param name="symbol">Filter by symbol.</param>
        /// <param name="account">Filter accounts that are either seller or buyer.</param>
        /// <param name="seller">Filter by seller.</param>
        /// <param name="buyer">Filter by buyer.</param>
        /// <param name="minPrice">Lower price limit.</param>
        /// <param name="maxPrice">Upper price limit.</param>
        /// <param name="minTemplateMint">Min template mint.</param>
        /// <param name="maxTemplateMint">Max template mint.</param>
        /// <param name="collectionName">Filter by collection name.</param>
        /// <param name="schemaName">Filter by schema name.</param>
        /// <param name="templateId">Filter by template ID.</param>
        /// <param name="burned">Filter for burned assets.</param>
        /// <param name="owner">Filter by owner.</param>
        /// <param name="match">Search for input in asset name on template data.</param>
        /// <param name="search">Fuzzy search for input in asset name on template data.</param>
        /// <param name="matchImmutableName">Search for input in asset name on asset immutable data.</param>
        /// <param name="matchMutableName">Search for input in asset name on asset mutable data.</param>
        /// <param name="isTransferable">Check if asset is transferable.</param>
        /// <param name="isBurnable">Check if asset is burnable.</param>
        /// <param name="collectionBlacklist">Hide collections from result.</param>
        /// <param name="collectionWhitelist">Show only results from specific collections.</param>
        /// <param name="ids">Show only results with matched ids.</param>
        /// <param name="lowerBound">Lower bound of primary key (value included).</param>
        /// <param name="upperBound">Upper bound of primary key (value excluded).</param>
        /// <param name="before">Only show results before this timestamp (value excluded).</param>
        /// <param name="after">Only show results after this timestamp (value excluded).</param>
        /// <param name="order">Order direction. Available values: "asc", "desc".</param>
        /// <param name="sort">Column to sort. Available values: "created", "updated", "sale_id", "price", "template_mint".</param>
        /// <param name="maxPages">Maxmimum number of pages (of size <see cref="AtomicClientOptions.PageSize" /> to return. Zero (0) means "all".</param>
        Task<List<Sale>> GetSales(
            ICollection<int>? state = null,
            int? maxAssets = null,
            int? minAssets = null,
            bool? showSellerContract = null,
            ICollection<string>? contractWhitelist = null,
            ICollection<string>? sellerBlacklist = null,
            ICollection<string>? buyerBlacklist = null,
            long? assetId = null,
            ICollection<string>? marketplace = null,
            ICollection<string>? makerMarketplace = null,
            ICollection<string>? takerMarketplace = null,
            string? symbol = null,
            string? account = null,
            ICollection<string>? seller = null,
            ICollection<string>? buyer = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minTemplateMint = null,
            int? maxTemplateMint = null,
            string? collectionName = null,
            string? schemaName = null,
            int? templateId = null,
            bool? burned = null,
            string? owner = null,
            string? match = null,
            string? search = null,
            string? matchImmutableName = null,
            string? matchMutableName = null,
            bool? isTransferable = null,
            bool? isBurnable = null,
            ICollection<string>? collectionBlacklist = null,
            ICollection<string>? collectionWhitelist = null,
            ICollection<int>? ids = null,
            int? lowerBound = null,
            int? upperBound = null,
            DateTimeOffset? before = null,
            DateTimeOffset? after = null,
            string? order = "desc",
            string? sort = "created",
            int maxPages = 0);

        /// <summary>
        /// Get the cheapest sale grouped by templates.
        /// </summary>
        /// <param name="symbol">Filter by symbol.</param>
        /// <param name="minPrice">Lower price limit.</param>
        /// <param name="maxPrice">Upper price limit.</param>
        /// <param name="collectionName">Filter by collection name.</param>
        /// <param name="schemaName">Filter by schema name.</param>
        /// <param name="templateId">Filter by template ID.</param>
        /// <param name="burned">Filter for burned assets.</param>
        /// <param name="owner">Filter by owner.</param>
        /// <param name="match">Search for input in asset name on template data.</param>
        /// <param name="search">Fuzzy search for input in asset name on template data.</param>
        /// <param name="matchImmutableName">Search for input in asset name on asset immutable data.</param>
        /// <param name="matchMutableName">Search for input in asset name on asset mutable data.</param>
        /// <param name="isTransferable">Check if asset is transferable.</param>
        /// <param name="isBurnable">Check if asset is burnable.</param>
        /// <param name="collectionBlacklist">Hide collections from result.</param>
        /// <param name="collectionWhitelist">Show only results from specific collections.</param>
        /// <param name="ids">Show only results with matched ids.</param>
        /// <param name="lowerBound">Lower bound of primary key (value included).</param>
        /// <param name="upperBound">Upper bound of primary key (value excluded).</param>
        /// <param name="before">Only show results before this timestamp (value excluded).</param>
        /// <param name="after">Only show results after this timestamp (value excluded).</param>
        /// <param name="order">Order direction. Available values: "asc", "desc".</param>
        /// <param name="sort">Column to sort. Available values: "template_id", "price".</param>
        /// <param name="maxPages">Maxmimum number of pages (of size <see cref="AtomicClientOptions.PageSize" /> to return. Zero (0) means "all".</param>
        Task<List<Sale>> GetSalesByTemplate(
            string symbol,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? collectionName = null,
            string? schemaName = null,
            int? templateId = null,
            bool? burned = null,
            string? owner = null,
            string? match = null,
            string? search = null,
            string? matchImmutableName = null,
            string? matchMutableName = null,
            bool? isTransferable = null,
            bool? isBurnable = null,
            ICollection<string>? collectionBlacklist = null,
            ICollection<string>? collectionWhitelist = null,
            ICollection<int>? ids = null,
            int? lowerBound = null,
            int? upperBound = null,
            DateTimeOffset? before = null,
            DateTimeOffset? after = null,
            string? order = "desc",
            string? sort = "template_id",
            int maxPages = 0);
    }
}
