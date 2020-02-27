using LyricsCounter.Service.Api.Client;
using LyricsCounter.Service.Entities;
using LyricsCounter.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricsCounter.Service
{
    public class SongLookupService : ISongLookupService
    {
        private readonly IMusicBrainzApiClient _apiClient;
        private readonly ILyricsApiClient _lyricsApiClient;
        private readonly ILyricsCounterRepository _lyricsCounterRepository;

        public SongLookupService(IMusicBrainzApiClient apiClient, ILyricsApiClient lyricsApiClient, ILyricsCounterRepository lyricsCounterRepository)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _lyricsApiClient = lyricsApiClient ?? throw new ArgumentNullException(nameof(lyricsApiClient));
            _lyricsCounterRepository = lyricsCounterRepository ?? throw new ArgumentNullException(nameof(lyricsCounterRepository));
        }

        public async Task<ArtistLookupResult> SearchArtist(string artist)
        {
            var results = await _apiClient.SearchArtistsByName(artist);

            return results;
        }

        public async Task<TrackList> GetSongsForArtist(Guid MBId)
        {
            //get the works results from the Api for the artist
            var worksResults = await _apiClient.LookupWorksForArtistByMBId(MBId);

            //extract the titles from the results and return a simpler tracklist object
            return new TrackList
            {
                Artist = worksResults.Name,
                MBId = MBId,
                Tracks = worksResults.Works.Select(p => p.Title)
            };
        }

        public async Task<ArtistLyricsAverage> CountLyricsForArtist(Guid MBId, IProgress<int> progress)
        {
            var trackList = await GetSongsForArtist(MBId);

            if (trackList == null || !trackList.Tracks.Any())
            {
                return null;
            }

            int totalTracks = trackList.Tracks.Count();

            int currentTrackCount = 0;
            foreach (var track in trackList.Tracks)
            {
                
                try
                {
                    //call Api to get results
                    //getting a fair few NotFound exceptions raised for artist/track so ignoring exceptions for now
                    var results = await _lyricsApiClient.GetLyrics(trackList.Artist, track);

                    //add the results to our data repository so we can access them later
                    await _lyricsCounterRepository.AddResults(new ArtistLyricsResult
                    {
                        Artist = trackList.Artist,
                        Lyrics = results.Lyrics,
                        MBId = MBId,
                        Song = track,
                        WordCount = CountWords(results.Lyrics)
                    });
                }
                catch
                {

                }

                //update our progress with current percent completion, it may be a slow operation..
                if (progress != null)
                {
                    currentTrackCount++;
                    progress.Report(currentTrackCount * 100 / totalTracks);
                }
            }
            return await _lyricsCounterRepository.GetAverageLyricsCountForArtist(trackList.Artist);
        }

        private int CountWords(string text)
        {
            if (String.IsNullOrEmpty(text))
                return 0;

            int wordCount = 0;
            int index = 0;

            while (index < text.Length)
            {
                if (char.IsWhiteSpace(text[index]))
                    wordCount++;
                index++;
            }
            return wordCount;
        }

        public async Task<IEnumerable<ArtistLyricsAverage>> GetHistory()
        {
            var result = await _lyricsCounterRepository.GetAllAverageLyrics();
            return result;
        }

        public async Task<ArtistLyricsAverage> GetArtist(string artist)
        {
            return await _lyricsCounterRepository.GetAverageLyricsCountForArtist(artist);
        }
    }
}
