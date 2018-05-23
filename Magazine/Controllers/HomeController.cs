using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index(string filter, MagazinesVirtualModels _magazineStoreVirtualModels)
        {
            if (filter != string.Empty && filter != null)
            {
                List<NsoupMagazineModels> nsoupModels;
                HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/" + filter.ToString()).Result;
                nsoupModels = httpResponseMessage.Content.ReadAsAsync<List<NsoupMagazineModels>>().Result;
                ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
                return View(nsoupModels);
            }
            ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
            @ViewBag.Message=Resources.Resource.MnsHomeFilterMgz;
            return View();
        }
        public ActionResult Save(List<NsoupMagazineModels> lstMagazines)
        {
            try
            {
                foreach (var mgz in lstMagazines)
                {
                    //_magazine(nsoupGetDataMgz(mgz.Url));

                    MagazineModels _magazine = new MagazineModels();
                    _magazine.Country = mgz.Country;
                    _magazine.Publisher = mgz.Publisher;
                    _magazine.PublicationType = mgz.Url;
                    _magazine.Subjec = mgz.Name;

                    HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.PostAsJsonAsync("Magazines", _magazine).Result;
                    MagazineModels magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;
                }

                return RedirectToAction("Index", "Magazine");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
                //return View("Index");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}