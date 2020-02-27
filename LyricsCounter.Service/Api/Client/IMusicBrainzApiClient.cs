using LyricsCounter.Service.Entities;
using LyricsCounter.Service.Entities.ServiceDefinition;
using System;
using System.Threading.Tasks;

namespace LyricsCounter.Service.Api.Client
{
    public interface IMusicBrainzApiClient
    {
        /// <summary>
        /// Query the MusicBrainz API for the specified Artist
        /// </summary>
        /// <param name="artist">The artist name</param>
        Task<ArtistLookupResult> SearchArtistsByName(string artist);

        /// <summary>
        /// Query the MusicBrainz API for a list of works by the specified artist MBID
        /// </summary>
        /// <param name="MBId">The Artists MBID</param>
        Task<WorksLookupResult> LookupWorksForArtistByMBId(Guid MBId);
    }
}