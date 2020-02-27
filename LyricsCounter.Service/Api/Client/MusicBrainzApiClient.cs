using LyricsCounter.Service.Entities;
using LyricsCounter.Service.Entities.ServiceDefinition;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LyricsCounter.Service.Api.Client
{
    public class MusicBrainzApiClient : ApiClientBase, IMusicBrainzApiClient
    {
        public MusicBrainzApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("C# App");
        }

        public async Task<WorksLookupResult> LookupWorksForArtistByMBId(Guid MBId)
        {
            string url = $"artist/{MBId.ToString()}?inc=works&fmt=json";
            var result = await this.GetAsync<WorksLookupResult>(url);

            return result;
        }

        public async Task<ArtistLookupResult> SearchArtistsByName(string artist)
        {
            if (String.IsNullOrEmpty(artist))
            {
                throw new ArgumentNullException(nameof(artist));
            }

            string url = $"artist/?query=artist:{Uri.EscapeDataString(artist)}&fmt=json";

            var result = await this.GetAsync<ArtistLookupResult>($"{url}");

            return result;
        }
    }
}
