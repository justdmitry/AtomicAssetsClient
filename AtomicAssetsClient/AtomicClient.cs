namespace AtomicAssetsClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AtomicAssetsClient.Data;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class AtomicClient : AtomicClientBase, IAtomicClient
    {
        public AtomicClient(ILogger<AtomicClient> logger, IOptions<AtomicClientOptions> options, HttpClient httpClient)
            : base(logger, options.Value, httpClient)
        {
            // Nothing
        }

        public Task<Asset> GetAsset(long assetId)
        {
            var uri = $"/atomicassets/v1/assets/{assetId}";
            return ExecuteGetRequest<Asset>(uri);
        }

        public Task<List<Asset>> GetAssets(int templateId, int maxPages = 0)
        {
            var uri = $"/atomicassets/v1/assets?template_id={templateId}";
            return ExecuteGetListRequest<Asset>(uri, maxPages);
        }

        public Task<Collection> GetCollection(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            var uri = $"/atomicassets/v1/collections/{collectionName}";
            return ExecuteGetRequest<Collection>(uri);
        }

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

        public Task<List<Schema>> GetSchemas(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            var uri = $"/atomicassets/v1/schemas?collection_name={collectionName}";
            return ExecuteGetListRequest<Schema>(uri, 0);
        }

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

        public Task<List<Template>> GetTemplates(string? collectionName = null, int maxPages = 0)
        {
            var uri = string.IsNullOrEmpty(collectionName)
                ? $"/atomicassets/v1/templates"
                : $"/atomicassets/v1/templates?collection_name={collectionName}";
            return ExecuteGetListRequest<Template>(uri, maxPages);
        }

        public Task<List<Template>> GetTemplates(params int[] templateIds)
        {
            var uri = $"/atomicassets/v1/templates?ids={string.Join(',', templateIds)}";
            return ExecuteGetListRequest<Template>(uri, 0);
        }
    }
}
