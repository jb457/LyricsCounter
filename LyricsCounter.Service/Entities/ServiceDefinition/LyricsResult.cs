using Newtonsoft.Json;

namespace LyricsCounter.Service.Entities.ServiceDefinition
{
    public class LyricsResult
    {
        [JsonProperty("lyrics")]
        public string Lyrics { get; set; }
    }
}
