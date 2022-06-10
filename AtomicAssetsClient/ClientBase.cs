namespace AtomicAssetsClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using AtomicAssetsClient.Data;
    using Microsoft.Extensions.Logging;

    public abstract class ClientBase
    {
        private readonly ILogger logger;
        private readonly ClientOptions options;
        private readonly HttpClient httpClient;

        private readonly SemaphoreSlim syncObject = new SemaphoreSlim(1);

        private DateTimeOffset sleepTill = DateTimeOffset.MinValue;

        protected ClientBase(ILogger logger, ClientOptions options, HttpClient httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            this.httpClient.BaseAddress = this.options.Endpoint;
        }

        protected async Task<List<T>> ExecuteGetListRequest<T>(string baseUri, int maxPages)
        {
            var res = new List<T>();
            var page = 0;

            while(true)
            {
                page++;
                var uri = baseUri + (baseUri.Contains('?') ? "&page=" : "?page=") + page;
                var items = await ExecuteGetRequest<List<T>>(uri).ConfigureAwait(false);

                if (items.Count == 0)
                {
                    break;
                }

                res.AddRange(items);

                if (maxPages > 0 && page >= maxPages)
                {
                    break;
                }
            }

            logger.LogDebug("Loaded {Count} of {Class} in {Count} pages", res.Count, typeof(T).Name, page);

            return res;
        }

        protected async Task<T> ExecuteGetRequest<T>(string uri)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, uri);
            var res = await ExecuteRequest<T>(req).ConfigureAwait(false);
            return res;
        }

        protected async Task<T> ExecuteRequest<T>(HttpRequestMessage request)
        {
            await syncObject.WaitAsync();

            try
            {
                if (DateTimeOffset.UtcNow < sleepTill)
                {
                    var sleepFor = sleepTill - DateTimeOffset.UtcNow;
                    logger.LogWarning("Too much requests, sleeping for {SleepFor}", sleepFor);
                    await Task.Delay(sleepFor);
                }

                var (data, headers) = await ExecuteRequestSafe<T>(request).ConfigureAwait(false);

                var requestLimit = -1;
                var requestLimitRemaining = -1;
                var requestLimitReset = DateTimeOffset.MinValue;

                if (headers.TryGetValues("X-RateLimit-Limit", out var requestLimitValues))
                {
                    _ = int.TryParse(requestLimitValues.First(), out requestLimit);
                }

                if (requestLimit > 0)
                {
                    if (headers.TryGetValues("X-RateLimit-Remaining", out var requestLimitRemaningValues))
                    {
                        _ = int.TryParse(requestLimitRemaningValues.First(), out requestLimitRemaining);
                    }

                    if (headers.TryGetValues("X-RateLimit-Reset", out var requestLimitResetValues)
                        && long.TryParse(requestLimitResetValues.First(), out var requestLimitResetValue))
                    {
                        requestLimitReset = DateTimeOffset.FromUnixTimeSeconds(requestLimitResetValue);
                    }

                    sleepTill = (requestLimitRemaining >= 0 && requestLimitRemaining < 3) ? requestLimitReset : DateTimeOffset.MinValue;
                    logger.LogDebug("Limits: remaining {Available} of {Limit}, reset in {Count} seconds", requestLimitRemaining, requestLimit, requestLimitReset.Subtract(DateTimeOffset.UtcNow).TotalSeconds);
                }

                return data;
            }
            finally
            {
                syncObject.Release();
            }
        }

        protected async Task<(T data, HttpResponseHeaders headers)> ExecuteRequestSafe<T>(HttpRequestMessage request)
        {
            using var resp = await httpClient.SendAsync(request).ConfigureAwait(false);

            var respText = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!resp.IsSuccessStatusCode)
            {
                logger.LogDebug("Request:  {Method} {Uri}", request.Method, request.RequestUri);
                logger.LogDebug("Response:\r\n{Text}", respText.SubstringSafe(200));

                resp.EnsureSuccessStatusCode(); // and throw error
            }

            var data = System.Text.Json.JsonSerializer.Deserialize<AtomicResponse<T>>(respText, options.JsonOptions);

            if (data == null || !data.Success)
            {
                logger.LogDebug("Request:  {Method} {Uri}", request.Method, request.RequestUri);
                logger.LogDebug("Response:\r\n{Text}", respText.SubstringSafe(200));
                throw new HttpRequestException("API call failed");
            }

            return (data.Data, resp.Headers);
        }
    }
}
