using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services
{
    public class QuartilesModels
    {
        [JsonProperty("Category")]
        public string Category { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("Quartile")]
        public string Quartile { get; set; }
        
    }
}