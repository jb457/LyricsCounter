using System;
using System.Collections.Generic;
using LyricsCounter.Service.Entities;

namespace LyricsCounter.Web.Models
{
    public class ArtistResultsModel
    {
        public ArtistResultsModel(ArtistLookupResult searchResults)
        {
            foreach (var artist in searchResults.Artists)
            {
                Artists.Add(new ArtistModel
                {
                    Name = artist.Name,
                    MBId = artist.Id,
                    Score = artist.Score
                });
            }
        }

        public IList<ArtistModel> Artists { get; set; } = new List<ArtistModel>();
    }

    public class ArtistModel
    {
        public string Name { get; set; }
        public Guid MBId { get; set; }
        public long Score { get; set; }
    }
}
