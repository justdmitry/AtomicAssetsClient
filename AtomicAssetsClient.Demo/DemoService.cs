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
            var templates = await client.GetTemplates(null, 2).ConfigureAwait(false);

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
