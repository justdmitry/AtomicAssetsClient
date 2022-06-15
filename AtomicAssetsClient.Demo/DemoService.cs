using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AtomicAssetsClient.Demo
{
    public class DemoService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IAtomicClient client;

        public DemoService(ILogger<DemoService> logger, IAtomicClient client)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var collection = await client.GetCollection("atomic").ConfigureAwait(false);
            logger.LogInformation("Collection '{Name}' ({FullName}) created by '{Author}' at block {Block}", collection.CollectionName, collection.Name, collection.Author, collection.CreatedAtBlock);

            collection = await client.GetCollectionOrDefault("not-exist").ConfigureAwait(false);
            if (collection == null)
            {
                logger.LogInformation("Collection 'not-exist' does not exist");
            }

            var templates = await client.GetTemplates(maxPages: 2).ConfigureAwait(false);

            foreach(var t in templates)
            {
                logger.LogInformation("In {Schema} id={Template} created in block {Block} as {Time}", t.Schema?.SchemaName, t.TemplateId, t.CreatedAtBlock, t.CreatedAtTime);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
