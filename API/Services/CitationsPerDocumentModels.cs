using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services
{
    public class CitationsPerDocumentModels
    {
        [JsonProperty("CitesPerDocument")]
        public string CitesPerDocument { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}