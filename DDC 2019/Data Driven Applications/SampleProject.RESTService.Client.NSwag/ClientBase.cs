using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.RESTService.Client
{
    public class ClientBase
    {
        public ClientBase()
        {
        }
        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpRequestMessage());
        }
    }
}