using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class NsoupMagazine
    {
        /// <summary>
        /// Url (Cadena)
        /// </summary>
        [JsonProperty("Url")]
        [Required]
        public string Url { get; set; }
        /// <summary>
        /// Nombre (Cadena)
        /// </summary>
        [JsonProperty("Name")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// País (Cadena)
        /// </summary>
        [JsonProperty("Country")]
        [Required]
        public string Country { get; set; }
        /// <summary>
        /// Editor (Cadena)
        /// </summary>
        [JsonProperty("Publisher")]
        [Required]
        public string Publisher { get; set; }
        /// <summary>
        /// Imagen (Cadena)
        /// </summary>
        [JsonProperty("Image")]
        [Required]
        public string Image { get; set; }
        /// <summary>
        /// Checked (Bit)
        /// </summary>
        [JsonProperty("Checked")]
        [Required]
        public bool Checked { get; set; }
    }
}