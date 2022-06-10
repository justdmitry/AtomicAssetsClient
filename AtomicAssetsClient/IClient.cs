using System;
using AtomicAssetsClient.Data;

namespace AtomicAssetsClient
{
    public interface IClient
    {
        Task<Asset> GetAsset(long assetId);
        Task<List<Asset>> GetAssets(int templateId, int maxPages = 0);

        Task<Collection> GetCollection(string collectionName);

        Task<Schema> GetSchema(string collectionName, string schemaName);
        Task<List<Schema>> GetSchemas(string collectionName);

        Task<Template> GetTemplate(string collectionName, int templateId);
        Task<List<Template>> GetTemplates(string? collectionName = null, int maxPages = 0);
        Task<List<Template>> GetTemplates(params int[] templateIds);
    }
}
