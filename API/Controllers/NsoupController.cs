using API._30.DistributedServices;
using API.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    public class NsoupController : ApiController
    {

        ////string base_url= "http://scimagojr.com/journalsearch.php?q=";
        ////string parameter = "09505849";
        //string parameter = "Kaunas University of Technology";

        //// GET: api/Nsoup
        //public List<NsoupMagazine> Get()
        //{

        //    NsoupServices nsoupService = new NsoupServices();
        //    return nsoupService.FetchScimagojr(parameter);
        //}


        // GET: api/Nsoup
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Nsoup/5
        public List<NsoupMagazine> GetFilter(string filter)
        {
            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.FetchScimagojr(filter);
        }

        // GET: api/Nsoup/GetDataMagazine/5
        public Magazine GetDataMagazine(string url)
        {
            
            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.GedDataMagazine(url);
        }


        // POST: api/Nsoup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Nsoup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Nsoup/5
        public void Delete(int id)
        {
        }
    }
}
