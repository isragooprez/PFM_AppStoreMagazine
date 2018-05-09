using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Magazine
{
    public class GlobalVarApi
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalVarApi()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:56110/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}