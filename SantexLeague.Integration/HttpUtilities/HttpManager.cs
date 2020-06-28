using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SantexLeague.Integration.HttpUtilities
{
    public class HttpManager : IHttpManager
    {
        
        private readonly IConfiguration config;

        public HttpManager(IConfiguration config)
        {
            this.config = config;
        }

        private HttpClient httpClient => new HttpClient();
        
        public async Task<T> Get<T>(string urlCommand)
        {
            T response = default;
            try
            {
                var client = this.httpClient;
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

        //public async Task<T> Post<T, U>(string urlCommand, U serializableEntity)
        //{
        //    T response = default;
        //    try
        //    {
        //        httpClient.DefaultRequestHeaders.Add("x-auth-token", this.config["ApiToken"] ?? string.Empty);
        //        HttpResponseMessage result = await httpClient.PostAsync(urlCommand, SerializeEntity(serializableEntity));
        //        response = await DeserializeToObject<T>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log
        //    }
        //    return response;
        //}

        private async Task<T> DeserializeToObject<T>(HttpResponseMessage message) {
            var jsonResult = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResult);
        }
        //private StringContent SerializeEntity<T>(T entity)
        //{
        //    return new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //}
    }
}