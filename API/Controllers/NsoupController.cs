using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class NsoupController : ApiController
    {
        string base_url= "http://scimagojr.com/journalsearch.php?q=09505849";

        // GET: api/Nsoup
        public string Get()
        {

            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.FetchScimagojr(base_url);
        }

        // GET: api/Nsoup/5
        public string Get(int id)
        {
            return "value";
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
