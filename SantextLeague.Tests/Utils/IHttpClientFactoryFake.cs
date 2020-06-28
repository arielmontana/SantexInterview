using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NSubstitute;

namespace SantextLeague.Tests.Utils
{
    public class IHttpClientFactoryFake
    {
        public static IHttpClientFactory Get<T>(T entities) 
        {
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpMessageHandler = new HttpMessageHandlerFake(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,                
                Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json")
            }); 
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);
            return httpClientFactoryMock;
        }

    }
}