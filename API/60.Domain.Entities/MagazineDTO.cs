using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API._60.Domain.Entities
{
    public class MagazineDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Subjec { get; set; }
        public string Publisher { get; set; }
        public string PublicationType { get; set; }
        public string ISSN { get; set; }
        public string Coverage { get; set; }
        public string Scope { get; set; }
        public string Quartiles { get; set; }
        public string SJR { get; set; }
        public string CitationsPerDocument { get; set; }
        public string Cites { get; set; }
        public string InternationalCollaboration { get; set; }
        public string DocumentsNoncitable { get; set; }
        public string DocumentsUncited { get; set; }
        public string TotalCites { get; set; }
    }
}