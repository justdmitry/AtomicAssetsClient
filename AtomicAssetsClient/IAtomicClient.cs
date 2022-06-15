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
    }
}
