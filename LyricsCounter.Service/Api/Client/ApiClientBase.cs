using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LyricsCounter.Service.Api.Client
{
    public abstract class ApiClientBase
    {
        protected HttpClient _httpClient;

        protected async Task<T> GetAsync<T>(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            return await SendAsync<T>(request);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            var result = await _httpClient.SendAsync(request);

            if (result.IsSuccessStatusCode)
            {
                //Hurray it worked.
                return await DeserializeJsonResponse<T>(result);
            }
            else
            {
                throw new ApplicationException($"Error received from API. Status: {result.StatusCode}");
            }
        }

        private async Task<T> DeserializeJsonResponse<T>(HttpResponseMessage response)
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings());

            var responseStream = await response.Content.ReadAsStreamAsync();
            using (var reader = new JsonTextReader(new StreamReader(responseStream)))
            {
                T result = serializer.Deserialize<T>(reader);

                if (result == null)
                {
                    throw new JsonException("Unable to de-serialise data");
                }

                return result;
            }
        }
    }
}
