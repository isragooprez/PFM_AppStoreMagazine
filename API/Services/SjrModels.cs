using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services
{
    public class SjrModels
    {
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("Sjr")]
        public string SJR { get; set; }
    }
}