using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class NsoupMagazine
    {
      
        [JsonProperty("Url")]
        public string Url { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Publisher")]
        public string Publisher { get; set; }
        [JsonProperty("Image")]
        public string Image { get; set; }
        [JsonProperty("Checked")]
        public bool Checked { get; set; }
    }
}