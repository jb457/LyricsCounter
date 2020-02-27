using LyricsCounter.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricsCounter.Service.Repository
{
    /// <summary>
    /// A simple form of storage, would be more ideal in a database
    /// </summary>
    public class LyricsCounterRepository : ILyricsCounterRepository
    {
        private static IDictionary<string, List<ArtistLyricsResult>> lyricsResults = new Dictionary<string, List<ArtistLyricsResult>>();

        public async Task AddResults(ArtistLyricsResult results)
        {
            List<ArtistLyricsResult> list = null;

            if (lyricsResults.ContainsKey(results.Artist))
            {
                list = lyricsResults[results.Artist];
                if (list == null)
                {
                    list = new List<ArtistLyricsResult>();
                }
            }
            else
            {
                list = new List<ArtistLyricsResult>();
            }
            list.Add(results);
            lyricsResults[results.Artist] = list;
        }

        public async Task<IEnumerable<ArtistLyricsAverage>> GetAllAverageLyrics()
        {
            List<ArtistLyricsAverage> list = new List<ArtistLyricsAverage>();

            foreach (var artist in lyricsResults.Keys)
            {
                list.Add(await GetAverageLyricsCountForArtist(artist));
            }

            return list;
        }

        public async Task<ArtistLyricsAverage> GetAverageLyricsCountForArtist(string artist)
        {
            if (lyricsResults.ContainsKey(artist))
            {
                var lyricsList = lyricsResults[artist];

                if (lyricsList == null)
                    return null;

                return new ArtistLyricsAverage
                {
                    Artist = artist,
                    AverageWordCount = lyricsList.Average(p => p.WordCount),
                    TracksCounted = lyricsList.Count
                };
            }

            return null;
        }
    }
}
