using OpenSearch.Client;
using OpenSearch.Net;

namespace SearchApiService.Services
{
    public static class OpenSearchClientFactory
    {
        public static IOpenSearchClient openSearchClient(IConfiguration config)
        {
            var (uri, user, pw, defaultIdx) = (
                config.GetSection("OpenSearch:Uri").Value,
                config.GetSection("OpenSearch:Username").Value,
                config.GetSection("OpenSearch:Password").Value,
                config.GetSection("OpenSearch:DefaultIndex").Value
            );

            var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri(uri ?? "")))
                    .BasicAuthentication(user, pw).DefaultIndex(defaultIdx);

            return new OpenSearchClient(settings);
        }
    }
}
