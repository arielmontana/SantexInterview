using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SantexLeague.Integration.ExternalEntities
{
    [Serializable]
    public class CompetitionDto
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("area")]
        public AreaDto area { get; set; }
        
        [JsonProperty("name")]
        public string name { get; set; }
        
        [JsonProperty("code")]
        public string code { get; set; }

    }
}