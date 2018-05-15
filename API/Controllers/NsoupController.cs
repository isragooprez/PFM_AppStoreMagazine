﻿using API.Models;
using API.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class NsoupController : ApiController
    {
        string base_url= "http://scimagojr.com/journalsearch.php?q=";
        //string parameter = "09505849";
        string parameter = "Kaunas University of Technology";

        // GET: api/Nsoup
        public List<NsoupMagazine> Get()
        {
           
            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.FetchScimagojr(parameter);
        }

        // GET: api/Nsoup/kanus
        public List<NsoupMagazine> Get(string filter)
        {
            NsoupServices nsoupService = new NsoupServices();
            return nsoupService.FetchScimagojr(filter);
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
