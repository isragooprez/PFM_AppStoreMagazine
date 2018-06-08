using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazine.Models
{
    public class MagazineStoreModels
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Imagen")]
        public string Imagen { get; set; }
        [JsonProperty("Hindexnumber")]
        public string Hindexnumber { get; set; }
        [JsonProperty("Hindexlegend")]
        public string Hindexlegend { get; set; }
        [JsonProperty("Url")]
        public string Url { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Subjec")]
        public string Subjec { get; set; }
        [JsonProperty("Publisher")]
        public string Publisher { get; set; }
        [JsonProperty("PublicationType")]
        public string PublicationType { get; set; }
        [JsonProperty("ISSN")]
        public string ISSN { get; set; }
        [JsonProperty("Coverage")]
        public string Coverage { get; set; }
        [JsonProperty("Scope")]
        public string Scope { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("DateIn")]
        public Nullable<System.DateTime> DateIn { get; set; }
        [JsonProperty("Favorite")]
        public Nullable<bool> Favorite { get; set; }
        [JsonProperty("QuartilesDesc")]
        public string QuartilesDesc { get; set; }
        [JsonProperty("SJRDesc")]
        public string SJRDesc { get; set; }
        [JsonProperty("CitationsPerDocumentDesc")]
        public string CitationsPerDocumentDesc { get; set; }
        [JsonProperty("CitesDesc")]
        public string CitesDesc { get; set; }
        [JsonProperty("InternationalCollaborationDesc")]
        public string InternationalCollaborationDesc { get; set; }
        [JsonProperty("DocumentsNoncitableDesc")]
        public string DocumentsNoncitableDesc { get; set; }
        [JsonProperty("DocumentsUncitedDesc")]
        public string DocumentsUncitedDesc { get; set; }
        [JsonProperty("TotalCitesDesc")]
        public string TotalCitesDesc { get; set; }
        [JsonProperty("Quartiles")]
        public string Quartiles { get; set; }
        [JsonProperty("SJR")]
        public string SJR { get; set; }
        [JsonProperty("CitationsPerDocument")]
        public string CitationsPerDocument { get; set; }
        [JsonProperty("Cites")]
        public string Cites { get; set; }
        [JsonProperty("InternationalCollaboration")]
        public string InternationalCollaboration { get; set; }
        [JsonProperty("DocumentsNoncitable")]
        public string DocumentsNoncitable { get; set; }
        [JsonProperty("DocumentsUncited")]
        public string DocumentsUncited { get; set; }
        [JsonProperty("TotalCites")]
        public string TotalCites { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
    }
}