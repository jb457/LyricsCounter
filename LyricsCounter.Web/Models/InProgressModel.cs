using LyricsCounter.Service.Entities;

namespace LyricsCounter.Web.Models
{
    public class InProgressModel
    {
        public string Artist { get; set; }

        public int PercentComplete { get; set; }

        public ArtistLyricsAverage Results { get; set; }
    }
}
