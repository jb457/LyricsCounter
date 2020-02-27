using LyricsCounter.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCounter.Service
{
    public interface ISongLookupService
    {
        /// <summary>
        /// Query MusicBrainz API for the artist, check it exists, get the MBID for it
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        Task<ArtistLookupResult> SearchArtist(string artist);

        /// <summary>
        /// Search MusicBrainz for a list of releases by the specified artist
        /// </summary>
        /// <param name="MBId">The MBID for the artist</param>
        /// <returns>a list of song titles</returns>
        Task<TrackList> GetSongsForArtist(Guid MBId);
        Task<IEnumerable<ArtistLyricsAverage>> GetHistory();

        Task<ArtistLyricsAverage> CountLyricsForArtist(Guid MBId, IProgress<int> progress);
        Task<ArtistLyricsAverage> GetArtist(string artist);
    }
}