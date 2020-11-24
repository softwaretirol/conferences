using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Client
{
    public abstract class ClientBase
    {
        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpRequestMessage());
        }
    }
}