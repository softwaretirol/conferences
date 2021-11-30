using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Client
{
    public class ClientBase
    {
        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HttpRequestMessage());
        }
    }
}
