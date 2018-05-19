using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazine.Models
{
    public class NsoupMagazineModels
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Publisher { get; set; }
        public string Image { get; set; }
    }
}