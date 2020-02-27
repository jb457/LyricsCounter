using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsCounter.Service.Entities
{
    public class ArtistLyricsResult
    {
        public string Artist { get; set; }
        public string Song { get; set; }
        public Guid MBId { get; set; }
        public string Lyrics { get; set; }
        public int WordCount { get; set; }
    }
}
