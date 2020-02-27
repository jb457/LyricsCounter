using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsCounter.Service.Entities.ServiceDefinition
{
    public partial class WorksLookupResult
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("works")]
        public Work[] Works { get; set; }

        [JsonProperty("begin-area")]
        public Area BeginArea { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("gender")]
        public object Gender { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("sort-name")]
        public string SortName { get; set; }

        [JsonProperty("gender-id")]
        public object GenderId { get; set; }

        [JsonProperty("end_area")]
        public object WelcomeEndArea { get; set; }

        [JsonProperty("begin_area")]
        public Area WelcomeBeginArea { get; set; }

        [JsonProperty("end-area")]
        public object EndArea { get; set; }

        [JsonProperty("life-span")]
        public LifeSpan LifeSpan { get; set; }

        [JsonProperty("type-id")]
        public Guid TypeId { get; set; }

        [JsonProperty("isnis")]
        public string[] Isnis { get; set; }

        [JsonProperty("ipis")]
        public object[] Ipis { get; set; }

        [JsonProperty("area")]
        public Area Area { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

    public partial class Area
    {
        [JsonProperty("iso-3166-1-codes", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Iso31661Codes { get; set; }

        [JsonProperty("sort-name")]
        public string SortName { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class LifeSpan
    {
        [JsonProperty("ended")]
        public bool Ended { get; set; }

        [JsonProperty("end")]
        public object End { get; set; }

        [JsonProperty("begin")]
        public string Begin { get; set; }
    }

    public partial class Work
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("attributes")]
        public object[] Attributes { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type-id")]
        public object TypeId { get; set; }

        [JsonProperty("iswcs")]
        public string[] Iswcs { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }
    }
}
