using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SantextLeague.Tests.Utils
{
    public class HttpMessageHandlerFake : DelegatingHandler
    {
        private HttpResponseMessage _fakeResponse;

        public HttpMessageHandlerFake(HttpResponseMessage responseMessage)
        {
            _fakeResponse = responseMessage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_fakeResponse);
        }
    }
}