using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class TotalCitesModels
    {
        [JsonProperty("Cites")]
        public string Cites { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}