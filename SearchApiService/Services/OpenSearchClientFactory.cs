using OpenSearch.Client;
using OpenSearch.Net;

namespace SearchApiService.Services
{
    public static class OpenSearchClientFactory
    {
        public static IOpenSearchClient openSearchClient(IConfiguration config)
        {
            var (uri, user, pw, defaultIdx) = (
                Environment.GetEnvironmentVariable("OPENSEARCH_URI") 
                    ?? config.GetSection("OpenSearch:Uri").Value,
                Environment.GetEnvironmentVariable("OPENSEARCH_USERNAME")
                    ?? config.GetSection("OpenSearch:Username").Value,
                Environment.GetEnvironmentVariable("OPENSEARCH_PASSWORD")
                    ?? config.GetSection("OpenSearch:Password").Value,
                Environment.GetEnvironmentVariable("OPENSEARCH_DefaultIndex")
                    ?? config.GetSection("OpenSearch:DefaultIndex").Value
            );

            var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri(uri ?? "")))
                    .BasicAuthentication(user, pw).DefaultIndex(defaultIdx);

            return new OpenSearchClient(settings);
        }
    }
}
