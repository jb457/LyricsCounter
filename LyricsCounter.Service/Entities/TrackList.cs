using System;
using System.Collections.Generic;

namespace LyricsCounter.Service.Entities
{
    public class TrackList
    {
        public string Artist { get; set; }
        public Guid MBId { get; set; }
        public IEnumerable<string> Tracks { get; set; }
    }
}
