using System;
using AtomicAssetsClient.Data;

namespace AtomicAssetsClient
{
    public interface IClient
    {
        Task<Template> GetTemplate(string collection, int templateId);
        Task<List<Template>> GetTemplates(string? collection = null, int maxPages = 0);
    }
}
