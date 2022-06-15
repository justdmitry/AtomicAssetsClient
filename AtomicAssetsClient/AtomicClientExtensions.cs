using System;
using AtomicAssetsClient.Data;

namespace AtomicAssetsClient
{
    public static class IAtomicClientExtensions
    {
        /// <summary>
        /// Uses <see cref="IAtomicClient.GetAssets" /> to find collection, returns null if not found.
        /// </summary>
        public static async Task<Asset?> GetAssetOrDefault(this IAtomicClient atomicClient, long id)
        {
            ArgumentNullException.ThrowIfNull(atomicClient);

            var list = await atomicClient.GetAssets(ids: new[] { id }, maxPages: 1).ConfigureAwait(false);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// Uses <see cref="IAtomicClient.GetCollections" /> to find collection, returns null if not found.
        /// </summary>
        public static async Task<Collection?> GetCollectionOrDefault(this IAtomicClient atomicClient, string name)
        {
            ArgumentNullException.ThrowIfNull(atomicClient);

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var list = await atomicClient.GetCollections(ids: new[] { name }, maxPages: 1).ConfigureAwait(false);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// Uses <see cref="IAtomicClient.GetSchemas" /> to find schema, returns null if not found.
        /// </summary>
        public static async Task<Schema?> GetSchemaOrDefault(this IAtomicClient atomicClient, string collectionName, string schemaName)
        {
            ArgumentNullException.ThrowIfNull(atomicClient);

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (string.IsNullOrWhiteSpace(schemaName))
            {
                throw new ArgumentNullException(nameof(schemaName));
            }

            var list = await atomicClient.GetSchemas(collectionName: collectionName, schemaName: schemaName, maxPages: 1).ConfigureAwait(false);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// Get all schemas in collection.
        /// </summary>
        public static Task<List<Schema>> GetSchemasInCollection(this IAtomicClient atomicClient, string collectionName)
        {
            ArgumentNullException.ThrowIfNull(atomicClient);

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return atomicClient.GetSchemas(collectionName: collectionName);
        }

        /// <summary>
        /// Uses <see cref="IAtomicClient.GetTemplates" /> to find template, returns null if not found.
        /// </summary>
        public static async Task<Template?> GetTemplateOrDefault(this IAtomicClient atomicClient, int templateId)
        {
            ArgumentNullException.ThrowIfNull(atomicClient);

            var list = await atomicClient.GetTemplates(ids: new[] { templateId }, maxPages: 1).ConfigureAwait(false);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// Get all templates in collection.
        /// </summary>
        public static Task<List<Template>> GetTemplatesInCollection(this IAtomicClient atomicClient, string collectionName)
        {
            ArgumentNullException.ThrowIfNull(atomicClient);

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return atomicClient.GetTemplates(collectionName: collectionName);
        }
    }
}
