using System.Collections.Generic;
using System.Threading.Tasks;
using LyricsCounter.Service.Entities;

namespace LyricsCounter.Service.Repository
{
    public interface ILyricsCounterRepository
    {
        Task AddResults(ArtistLyricsResult results);
        Task<ArtistLyricsAverage> GetAverageLyricsCountForArtist(string artist);
        Task<IEnumerable<ArtistLyricsAverage>> GetAllAverageLyrics();
    }
}