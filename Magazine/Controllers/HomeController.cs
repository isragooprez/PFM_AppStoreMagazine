using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Magazine.Content.Utils;

namespace Magazine.Controllers
{
    public class HomeController : Controller
    {

        MagazinesVirtualModels _magazineStoreVirtualModels;
        /**
         *Seguridades ValidateInput protege ataques XSS  activado por defecto los otros controladores no lo 
         * especifico por que ya esa activado automaticamente
         **/
        [ValidateInput(true)]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string filter, MagazinesVirtualModels _magazineStoreVirtualModels)
        {
            List<NsoupMagazineModels> nsoupModels;
            if (filter != string.Empty && filter != null)
            {
                var _filter = string.Empty;
                _filter = Utils.ClearTagsHtml(filter);

                HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/" + _filter.ToString()).Result;
                nsoupModels = httpResponseMessage.Content.ReadAsAsync<List<NsoupMagazineModels>>().Result;
                if (nsoupModels == null || nsoupModels.Count() <= 0)
                {
                    TempData["ErrorMessage"] = Resources.Resource.MsnMgzErrorNotFoundData;
                }
                ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
                return View(nsoupModels);
            }
            ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
            ViewBag.Message = Resources.Resource.MnsHomeFilterMgz;
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

                ViewBag.TotalMgzVirtual= (_magazineStoreVirtualModels ==null)?0: _magazineStoreVirtualModels.Count(); 
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.TotalMgzVirtual = (_magazineStoreVirtualModels == null) ? 0 : _magazineStoreVirtualModels.Count();

            return View();
        }



    }
}