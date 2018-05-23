using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services
{
    public class InternationalCollaborationModels
    {
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty(" InternationalCollaboration")]
        public string InternationalCollaboration { get; set; }
    }
}