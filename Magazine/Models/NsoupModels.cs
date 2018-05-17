﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazine.Models
{
    public class NsoupModels
    {
        //[JsonProperty("Id")]
        public int Id { get; set; }
        //[JsonProperty("Country")]
        public string Country { get; set; }
        //[JsonProperty("Subjec")]
        public string Subjec { get; set; }
        //[JsonProperty("Publisher")]
        public string Publisher { get; set; }
        //[JsonProperty("PublicationType")]
        public string PublicationType { get; set; }
        //[JsonProperty("ISSN")]
        public string ISSN { get; set; }
        //[JsonProperty("Coverage")]
        public string Coverage { get; set; }
        //[JsonProperty("Scope")]
        public string Scope { get; set; }
        //[JsonProperty("Quartiles")]
        public string Quartiles { get; set; }
        //[JsonProperty("SJR")]
        public string SJR { get; set; }
        //[JsonProperty("CitationsPerDocument")]
        public string CitationsPerDocument { get; set; }
        //[JsonProperty("Cites")]
        public string Cites { get; set; }
        //[JsonProperty("InternationalCollaboration")]
        public string InternationalCollaboration { get; set; }
        //[JsonProperty("DocumentsNoncitable")]
        public string DocumentsNoncitable { get; set; }
        //[JsonProperty("DocumentsUncited")]
        public string DocumentsUncited { get; set; }
        //[JsonProperty("TotalCites")]
        public string TotalCites { get; set; }


    }
}