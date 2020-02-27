namespace LyricsCounter.Service.Entities
{
    public class ArtistLyricsAverage
    {
        public string Artist { get; set; }
        public double AverageWordCount { get; set; }
        public int TracksCounted { get; set; }
    }
}
