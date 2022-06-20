namespace AtomicAssetsClient
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using AtomicAssetsClient.Data;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class AtomicClient : AtomicClientBase, IAtomicClient
    {
        public AtomicClient(ILogger<AtomicClient> logger, IOptions<AtomicClientOptions> options, IHttpClientFactory httpClientFactory)
            : base(logger, options.Value, httpClientFactory)
        {
            // Nothing
        }

        /// <inheritdoc>
        public Task<Asset> GetAsset(long assetId)
        {
            var uri = $"/atomicassets/v1/assets/{assetId}";
            return ExecuteGetRequest<Asset>(uri);
        }

        /// <inheritdoc>
        public Task<List<Asset>> GetAssets(
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
            int maxPages = 0)
        {
            var sb = new StringBuilder("/atomicassets/v1/assets?", 150);

            AppendIfNotEmptyOrWhitespace(sb, collectionName, "collection_name");
            AppendIfNotEmptyOrWhitespace(sb, schemaName, "schema_name");
            AppendIfNotNull(sb, templateId, "template_id");
            AppendIfNotNull(sb, burned);
            AppendIfNotEmptyOrWhitespace(sb, owner);
            AppendIfNotEmptyOrWhitespace(sb, match);
            AppendIfNotEmptyOrWhitespace(sb, search);
            AppendIfNotEmptyOrWhitespace(sb, matchImmutableName, "match_immutable_name");
            AppendIfNotEmptyOrWhitespace(sb, matchMutableName, "match_mutable_name");
            AppendIfNotNull(sb, isTransferable, "is_transferable");
            AppendIfNotNull(sb, isBurnable, "is_burnable");
            AppendIfNotEmpty(sb, collectionBlacklist, "collection_blacklist");
            AppendIfNotEmpty(sb, collectionWhitelist, "collection_whitelist");
            AppendIfNotNull(sb, onlyDuplicateTemplates, "only_duplicate_templates");
            AppendIfNotNull(sb, hasBackedTokens, "has_backed_tokens");
            AppendIfNotEmptyOrWhitespace(sb, authorizedAccount, "authorized_account");
            AppendIfNotEmpty(sb, templateWhitelist, "template_whitelist");
            AppendIfNotEmpty(sb, templateBlacklist, "template_blacklist");
            AppendIfNotEmpty(sb, hideTemplatesByAccounts, "hide_templates_by_accounts");
            AppendIfNotNull(sb, hideOffers, "hide_offers");
            AppendIfNotEmpty(sb, ids);
            AppendIfNotNull(sb, lowerBound, "lower_bound");
            AppendIfNotNull(sb, upperBound, "upper_bound");
            AppendIfNotNull(sb, before);
            AppendIfNotNull(sb, after);

            if (order != "asc" && order != "desc")
            {
                throw new ArgumentException("Must be 'asc' or 'desc'", nameof(order));
            }
            else
            {
                sb.Append("&order=");
                sb.Append(order);
            }

            switch (sort)
            {
                case "asset_id":
                case "minted":
                case "updated":
                case "transferred":
                case "template_mint":
                case "name":
                    sb.Append("&sort=");
                    sb.Append(sort);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sort));
            }

            return ExecuteGetListRequest<Asset>(sb.ToString(), maxPages);
        }

        /// <inheritdoc>
        public Task<Collection> GetCollection(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            var uri = $"/atomicassets/v1/collections/{collectionName}";
            return ExecuteGetRequest<Collection>(uri);
        }

        /// <inheritdoc>
        public Task<List<Collection>> GetCollections(
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
            int maxPages = 0)
        {
            var sb = new StringBuilder("/atomicassets/v1/collections?", 150);

            AppendIfNotEmptyOrWhitespace(sb, author);
            AppendIfNotEmptyOrWhitespace(sb, match);
            AppendIfNotEmptyOrWhitespace(sb, authorizedAccount, "authorized_account");
            AppendIfNotEmptyOrWhitespace(sb, notifyAccount, "notify_account");
            AppendIfNotEmptyOrWhitespace(sb, author);
            AppendIfNotEmpty(sb, collectionBlacklist, "collection_blacklist");
            AppendIfNotEmpty(sb, collectionWhitelist, "collection_whitelist");
            AppendIfNotEmpty(sb, ids);
            AppendIfNotEmptyOrWhitespace(sb, lowerBound, "lower_bound");
            AppendIfNotEmptyOrWhitespace(sb, upperBound, "upper_bound");
            AppendIfNotNull(sb, before);
            AppendIfNotNull(sb, after);

            if (order != "asc" && order != "desc")
            {
                throw new ArgumentException("Must be 'asc' or 'desc'", nameof(order));
            }
            else
            {
                sb.Append("&order=");
                sb.Append(order);
            }

            if (sort != "created" && sort != "collection_name")
            {
                throw new ArgumentException("Must be 'created' or 'collection_name'", nameof(sort));
            }
            else
            {
                sb.Append("&sort=");
                sb.Append(sort);
            }

            return ExecuteGetListRequest<Collection>(sb.ToString(), maxPages);
        }

        /// <inheritdoc>
        public Task<CollectionStats> GetCollectionStats(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            var uri = $"/atomicassets/v1/collections/{collectionName}/stats";
            return ExecuteGetRequest<CollectionStats>(uri);
        }

        /// <inheritdoc>
        public Task<Schema> GetSchema(string collectionName, string schemaName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentNullException(nameof(schemaName));
            }

            var uri = $"/atomicassets/v1/schemas/{collectionName}/{schemaName}";
            return ExecuteGetRequest<Schema>(uri);
        }

        /// <inheritdoc>
        public Task<List<Schema>> GetSchemas(
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
            int maxPages = 0)
        {
            var sb = new StringBuilder("/atomicassets/v1/schemas?", 150);

            AppendIfNotEmptyOrWhitespace(sb, collectionName, "collection_name");
            AppendIfNotEmptyOrWhitespace(sb, authorizedAccount, "authorized_account");
            AppendIfNotEmptyOrWhitespace(sb, schemaName, "schema_name");
            AppendIfNotEmptyOrWhitespace(sb, match);
            AppendIfNotEmpty(sb, collectionBlacklist, "collection_blacklist");
            AppendIfNotEmpty(sb, collectionWhitelist, "collection_whitelist");
            AppendIfNotEmpty(sb, ids);
            AppendIfNotEmptyOrWhitespace(sb, lowerBound, "lower_bound");
            AppendIfNotEmptyOrWhitespace(sb, upperBound, "upper_bound");
            AppendIfNotNull(sb, before);
            AppendIfNotNull(sb, after);

            if (order != "asc" && order != "desc")
            {
                throw new ArgumentException("Must be 'asc' or 'desc'", nameof(order));
            }
            else
            {
                sb.Append("&order=");
                sb.Append(order);
            }

            if (sort != "created" && sort != "schema_name")
            {
                throw new ArgumentException("Must be 'created' or 'schema_name'", nameof(sort));
            }
            else
            {
                sb.Append("&sort=");
                sb.Append(sort);
            }

            return ExecuteGetListRequest<Schema>(sb.ToString(), maxPages);
        }

        /// <inheritdoc>
        public Task<SchemaStats> GetSchemaStats(string collectionName, string schemaName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentNullException(nameof(schemaName));
            }

            var uri = $"/atomicassets/v1/schemas/{collectionName}/{schemaName}/stats";
            return ExecuteGetRequest<SchemaStats>(uri);
        }

        /// <inheritdoc>
        public Task<Template> GetTemplate(string collectionName, int templateId)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (templateId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(templateId), "Must be positive");
            }

            var uri = $"/atomicassets/v1/templates/{collectionName}/{templateId}";
            return ExecuteGetRequest<Template>(uri);
        }

        /// <inheritdoc>
        public Task<List<Template>> GetTemplates(
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
            int maxPages = 0)
        {
            var sb = new StringBuilder("/atomicassets/v1/templates?", 150);

            AppendIfNotEmptyOrWhitespace(sb, collectionName, "collection_name");
            AppendIfNotEmptyOrWhitespace(sb, schemaName, "schema_name");
            AppendIfNotNull(sb, issuedSupply, "issued_supply");
            AppendIfNotNull(sb, minIssuedSupply, "min_issued_supply");
            AppendIfNotNull(sb, maxIssuedSupply, "max_issued_supply");
            AppendIfNotNull(sb, hasAssets, "has_assets");
            AppendIfNotNull(sb, maxSupply, "max_supply");
            AppendIfNotNull(sb, isBurnable, "is_burnable");
            AppendIfNotNull(sb, isTransferable, "is_transferable");
            AppendIfNotEmptyOrWhitespace(sb, authorizedAccount, "authorized_account");
            AppendIfNotEmptyOrWhitespace(sb, match);
            AppendIfNotEmpty(sb, collectionBlacklist, "collection_blacklist");
            AppendIfNotEmpty(sb, collectionWhitelist, "collection_whitelist");
            AppendIfNotEmpty(sb, ids);
            AppendIfNotNull(sb, lowerBound, "lower_bound");
            AppendIfNotNull(sb, upperBound, "upper_bound");
            AppendIfNotNull(sb, before);
            AppendIfNotNull(sb, after);

            if (order != "asc" && order != "desc")
            {
                throw new ArgumentException("Must be 'asc' or 'desc'", nameof(order));
            }
            else
            {
                sb.Append("&order=");
                sb.Append(order);
            }

            if (sort != "created" && sort != "name")
            {
                throw new ArgumentException("Must be 'created' or 'name'", nameof(sort));
            }
            else
            {
                sb.Append("&sort=");
                sb.Append(sort);
            }

            return ExecuteGetListRequest<Template>(sb.ToString(), maxPages);
        }

        /// <inheritdoc>
        public Task<TemplateStats> GetTemplateStats(string collectionName, int templateId)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (templateId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(templateId), "Must be positive");
            }

            var uri = $"/atomicassets/v1/templates/{collectionName}/{templateId}/stats";
            return ExecuteGetRequest<TemplateStats>(uri);
        }

        /// <inheritdoc>
        public Task<Sale> GetSale(int saleId)
        {
            var uri = $"/atomicmarket/v1/sales/{saleId}";
            return ExecuteGetRequest<Sale>(uri);
        }

        /// <inheritdoc>
        public Task<List<Sale>> GetSales(
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
            int maxPages = 0)
        {
            var sb = new StringBuilder("/atomicmarket/v2/sales?", 150);

            AppendIfNotEmpty(sb, state);
            AppendIfNotNull(sb, maxAssets, "max_assets");
            AppendIfNotNull(sb, minAssets, "min_assets");
            AppendIfNotNull(sb, showSellerContract, "show_seller_contracts");
            AppendIfNotEmpty(sb, contractWhitelist, "contract_whitelist");
            AppendIfNotEmpty(sb, sellerBlacklist, "seller_blacklist");
            AppendIfNotEmpty(sb, buyerBlacklist, "buyer_blacklist");
            AppendIfNotNull(sb, assetId, "asset_id");
            AppendIfNotEmpty(sb, marketplace);
            AppendIfNotEmpty(sb, makerMarketplace, "maker_marketplace");
            AppendIfNotEmpty(sb, takerMarketplace, "taker_marketplace");
            AppendIfNotEmptyOrWhitespace(sb, symbol);
            AppendIfNotEmptyOrWhitespace(sb, account);
            AppendIfNotEmpty(sb, seller);
            AppendIfNotEmpty(sb, buyer);
            AppendIfNotNull(sb, minPrice, "min_price");
            AppendIfNotNull(sb, maxPrice, "max_price");
            AppendIfNotNull(sb, minTemplateMint, "min_template_mint");
            AppendIfNotNull(sb, maxTemplateMint, "max_template_mint");
            AppendIfNotEmptyOrWhitespace(sb, collectionName, "collection_name");
            AppendIfNotEmptyOrWhitespace(sb, schemaName, "schema_name");
            AppendIfNotNull(sb, templateId, "template_id");
            AppendIfNotNull(sb, burned);
            AppendIfNotEmptyOrWhitespace(sb, owner);
            AppendIfNotEmptyOrWhitespace(sb, match);
            AppendIfNotEmptyOrWhitespace(sb, search);
            AppendIfNotEmptyOrWhitespace(sb, matchImmutableName, "match_immutable_name");
            AppendIfNotEmptyOrWhitespace(sb, matchMutableName, "match_mutable_name");
            AppendIfNotNull(sb, isTransferable, "is_transferable");
            AppendIfNotNull(sb, isBurnable, "is_burnable");
            AppendIfNotEmpty(sb, collectionBlacklist, "collection_blacklist");
            AppendIfNotEmpty(sb, collectionWhitelist, "collection_whitelist");
            AppendIfNotEmpty(sb, ids);
            AppendIfNotNull(sb, lowerBound, "lower_bound");
            AppendIfNotNull(sb, upperBound, "upper_bound");
            AppendIfNotNull(sb, before);
            AppendIfNotNull(sb, after);

            if (order != "asc" && order != "desc")
            {
                throw new ArgumentException("Must be 'asc' or 'desc'", nameof(order));
            }
            else
            {
                sb.Append("&order=");
                sb.Append(order);
            }

            switch (sort)
            {
                case "created":
                case "updated":
                case "sale_id":
                case "price":
                case "template_mint":
                    sb.Append("&sort=");
                    sb.Append(sort);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sort));
            }

            return ExecuteGetListRequest<Sale>(sb.ToString(), maxPages);
        }

        /// <inheritdoc>
        public Task<List<Sale>> GetSalesByTemplate(
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
            int maxPages = 0)
        {
            var sb = new StringBuilder("/atomicmarket/v1/sales/templates?", 150);

            AppendIfNotEmptyOrWhitespace(sb, symbol);
            AppendIfNotNull(sb, minPrice, "min_price");
            AppendIfNotNull(sb, maxPrice, "max_price");
            AppendIfNotEmptyOrWhitespace(sb, collectionName, "collection_name");
            AppendIfNotEmptyOrWhitespace(sb, schemaName, "schema_name");
            AppendIfNotNull(sb, templateId, "template_id");
            AppendIfNotNull(sb, burned);
            AppendIfNotEmptyOrWhitespace(sb, owner);
            AppendIfNotEmptyOrWhitespace(sb, match);
            AppendIfNotEmptyOrWhitespace(sb, search);
            AppendIfNotEmptyOrWhitespace(sb, matchImmutableName, "match_immutable_name");
            AppendIfNotEmptyOrWhitespace(sb, matchMutableName, "match_mutable_name");
            AppendIfNotNull(sb, isTransferable, "is_transferable");
            AppendIfNotNull(sb, isBurnable, "is_burnable");
            AppendIfNotEmpty(sb, collectionBlacklist, "collection_blacklist");
            AppendIfNotEmpty(sb, collectionWhitelist, "collection_whitelist");
            AppendIfNotEmpty(sb, ids);
            AppendIfNotNull(sb, lowerBound, "lower_bound");
            AppendIfNotNull(sb, upperBound, "upper_bound");
            AppendIfNotNull(sb, before);
            AppendIfNotNull(sb, after);

            if (order != "asc" && order != "desc")
            {
                throw new ArgumentException("Must be 'asc' or 'desc'", nameof(order));
            }
            else
            {
                sb.Append("&order=");
                sb.Append(order);
            }

            switch (sort)
            {
                case "template_id":
                case "price":
                    sb.Append("&sort=");
                    sb.Append(sort);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sort));
            }

            return ExecuteGetListRequest<Sale>(sb.ToString(), maxPages);
        }

        private static void AppendIfNotEmptyOrWhitespace(StringBuilder sb, string? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                sb.Append(value);
            }
        }

        private static void AppendIfNotEmpty(StringBuilder sb, ICollection<string>? values, [CallerArgumentExpression("values")] string? paramName = null)
        {
            if (values != null && values.Count > 0)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                var first = true;
                foreach (var val in values)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sb.Append(',');
                    }

                    sb.Append(val);
                }
            }
        }

        private static void AppendIfNotEmpty(StringBuilder sb, ICollection<int>? values, [CallerArgumentExpression("values")] string? paramName = null)
        {
            if (values != null && values.Count > 0)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                var first = true;
                foreach (var val in values)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sb.Append(',');
                    }

                    sb.Append(val.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private static void AppendIfNotEmpty(StringBuilder sb, ICollection<long>? values, [CallerArgumentExpression("values")] string? paramName = null)
        {
            if (values != null && values.Count > 0)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                var first = true;
                foreach (var val in values)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sb.Append(',');
                    }

                    sb.Append(val.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private static void AppendIfNotNull(StringBuilder sb, DateTimeOffset? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value != null)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                sb.Append(value.Value.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture));
            }
        }

        private static void AppendIfNotNull(StringBuilder sb, int? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value != null)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                sb.Append(value.Value.ToString(CultureInfo.InvariantCulture));
            }
        }

        private static void AppendIfNotNull(StringBuilder sb, long? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value != null)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                sb.Append(value.Value.ToString(CultureInfo.InvariantCulture));
            }
        }

        private static void AppendIfNotNull(StringBuilder sb, bool? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value != null)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append(value.Value ? "=true" : "=false");
            }
        }

        private static void AppendIfNotNull(StringBuilder sb, decimal? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value != null)
            {
                sb.Append('&');
                sb.Append(paramName);
                sb.Append('=');
                sb.Append(value.Value.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
