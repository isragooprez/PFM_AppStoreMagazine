using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DocumentsUncitedModels
    {
        [JsonProperty("Documents")]
        public string Documents { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}