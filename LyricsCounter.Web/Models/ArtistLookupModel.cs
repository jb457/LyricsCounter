using LyricsCounter.Service.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyricsCounter.Web.Models
{
    public class ArtistLookupModel
    {
        [Required]
        public string Artist { get; set; }

        public IEnumerable<ArtistLyricsAverage> History { get; set; }
    }
}
