using API._30.DistributedServices;
using API.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    public class NsoupController : ApiController
    {
        /// <summary>
        /// Get magazines given a topic or search criteria Example: "Software"
        /// </summary>
        /// <param name="filter">Search criteria</param>
        /// <returns>List magazines that consider the criteria</returns>
        // GET: api/Nsoup/5
        public List<NsoupMagazine> GetFilter(string filter)
        {
            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.FetchScimagojr(filter);
        }
        /// <summary>
        /// Obtain information from a journal in Html format, given the URL to be analyzed.
        /// </summary>
        /// <param name="url">Unique parameter of the magazines.</param>
        /// <returns>Object journal with your information analyzed.</returns>
        // GET: api/Nsoup/GetDataMagazine/5
        public Magazine GetDataMagazine(string url)
        {
            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.GedDataMagazine(url);
        }

    }
}
