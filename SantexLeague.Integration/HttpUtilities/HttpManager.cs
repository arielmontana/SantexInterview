using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SantexLeague.Integration.HttpUtilities
{
    public class HttpManager : IHttpManager
    {
        private readonly IConfiguration config;
        private readonly IHttpClientFactory httpClientFactory;

        public HttpManager(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            this.config = config;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> Get<T>(string urlCommand)
        {
            T response = default;
            try
            {
                var client = this.httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("x-auth-token", this.config["ApiToken"] ?? string.Empty);
                HttpResponseMessage result = await client.GetAsync(urlCommand);
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"Error '{result.StatusCode.ToString()}' trying to access to '{urlCommand}'");
                }
                response = await DeserializeToObject<T>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        private async Task<T> DeserializeToObject<T>(HttpResponseMessage message)
        {
            var jsonResult = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResult);
        }
    }
}