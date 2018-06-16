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

        MagazinesVirtualModels _magazineStoreVirtualModels = new MagazinesVirtualModels();
        NsoupVirtualModels _nsoupMagazineModels = new NsoupVirtualModels();
        /**
         *Seguridades ValidateInput protege ataques XSS  activado por defecto los otros controladores no lo 
         * especifico por que ya esa activado automaticamente
         **/
        [ValidateInput(true)]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string filter, MagazinesVirtualModels _magazineStoreVirtualModels, NsoupVirtualModels _nsoupMagazineModels)
        {
            List<NsoupMagazineModels> nsoupModels = new List<NsoupMagazineModels>();
            if (filter != string.Empty && filter != null)
            {
                var _filter = string.Empty;
                _filter = Utils.ClearTagsHtml(filter);
                _nsoupMagazineModels.Clear();
                HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/" + _filter.ToString()).Result;
                nsoupModels = httpResponseMessage.Content.ReadAsAsync<List<NsoupMagazineModels>>().Result;
                ViewBag.TotalMgzFilterVirtual = nsoupModels.Count();
                if (nsoupModels == null || nsoupModels.Count() <= 0)
                {
                    TempData["ErrorMessage"] = Resources.Resource.MsnMgzErrorNotFoundData;
                }
                if (_nsoupMagazineModels.Count() == 0)
                {
                    return View(StoreSearchInSession(nsoupModels, _magazineStoreVirtualModels, _nsoupMagazineModels));
                }
                ViewBag.TotalMgzFilterVirtual = _nsoupMagazineModels.Count();
                ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();


                return View(nsoupModels);
            }
            else
            {
                if (_nsoupMagazineModels.Count() > 0)
                {
                    ViewBag.TotalMgzFilterVirtual = _nsoupMagazineModels.Count();
                    return View(StoreSearchInSession(nsoupModels, _magazineStoreVirtualModels, _nsoupMagazineModels));

                }
            }
            ViewBag.TotalMgzFilterVirtual = _nsoupMagazineModels.Count();
            ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
            ViewBag.Message = Resources.Resource.MnsHomeFilterMgz;
            return View();
        }

        public List<NsoupMagazineModels> StoreSearchInSession(List<NsoupMagazineModels> nsoupModels, MagazinesVirtualModels _magazineStoreVirtualModels, NsoupVirtualModels _nsoupMagazineModels)
        {
            if (nsoupModels != null)
            {
                foreach (var nsoup in nsoupModels)
                {
                    _nsoupMagazineModels.Add(nsoup);
                }
                ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();

            }
            return _nsoupMagazineModels;
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

            ViewBag.TotalMgzVirtual = (_magazineStoreVirtualModels == null) ? 0 : _magazineStoreVirtualModels.Count();
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