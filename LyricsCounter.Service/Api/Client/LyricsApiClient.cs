using LyricsCounter.Service.Entities.ServiceDefinition;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LyricsCounter.Service.Api.Client
{
    public class LyricsApiClient : ApiClientBase, ILyricsApiClient
    {
        public LyricsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<LyricsResult> GetLyrics(string artistName, string songName)
        {
            string url = $"{Uri.EscapeDataString(artistName)}/{Uri.EscapeDataString(songName)}";

            var result = await this.GetAsync<LyricsResult>($"{url}");
            return result;
        }
    }
}
