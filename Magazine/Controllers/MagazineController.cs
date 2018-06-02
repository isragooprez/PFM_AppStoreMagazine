using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Magazine.Models;
using System.Linq.Dynamic;

namespace Magazine.Controllers
{
    public class MagazineController : Controller
    {
        // GET: Magazine
        public ActionResult Index(int page=1, string sort="Name", string sortdir="DESC", string search="")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var listMagazines = GetMagazinesSearch(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.Search = search;

            return View(listMagazines);
        }

        public List<MagazineModels> GetMagazinesSearch(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {

            IEnumerable<MagazineModels> listMagazines;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines").Result;
            listMagazines = httpResponseMessage.Content.ReadAsAsync<IEnumerable<MagazineModels>>().Result;

            var _mgznSearch = (from mgzn in listMagazines
                               where (mgzn.Name != null && mgzn.Country != null && mgzn.Subjec != null && mgzn.Publisher != null && mgzn.PublicationType != null && mgzn.ISSN != null && mgzn.Coverage != null && mgzn.DateIn != null && (mgzn.Favorite == null || mgzn.Favorite != null)) &&
                                 (
                                 mgzn.Name.Contains(search) ||
                                  mgzn.Country.Contains(search) ||
                                  mgzn.Subjec.Contains(search) ||
                                  mgzn.Publisher.Contains(search) ||
                                  mgzn.PublicationType.Contains(search) ||
                                  mgzn.ISSN.Contains(search) ||
                                  mgzn.Coverage.Contains(search) ||
                                  mgzn.DateIn.ToString().Contains(search) ||
                                  mgzn.Favorite.Contains(search))
                               select mgzn
                        );
            totalRecord = _mgznSearch.Count();
            _mgznSearch = _mgznSearch.OrderBy(sort + " " + sortdir);
            if (pageSize > 0)
            {
                _mgznSearch = _mgznSearch.Skip(skip).Take(pageSize);
            }
            return _mgznSearch.ToList();
        }

        // GET: Magazine/Details/5
        public ActionResult Details(int id)
        {
            //AspNetUsers aspNetUsers = factoryDAO.getRepositoryUsers().FindById(user => user.Id == id);
            MagazineModels magazine;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines/" + id).Result;
            magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;
            return View(magazine);
        }

        // GET: Magazine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Magazine/Create
        [HttpPost]
        public ActionResult Create(MagazineModels _magazine)
        {
            try
            {
                MagazineModels magazine;
                HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.PostAsJsonAsync("Magazines", _magazine).Result;
                magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Magazine/Edit/5
        public ActionResult Edit(int id)
        {
            MagazineModels magazine;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines/" + id.ToString()).Result;
            magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;
            return View(magazine);
        }
        //Control de Vulnerabilidad de Asignaciones Masivas (OverPosting) | Seguridad Web
        //[Bind(Include = "Description,Favorite,Scope")]
        // POST: Magazine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Description,Favorite,Scope")] MagazineModels _magazineViewModel)
        {
            try
            {
                _magazineViewModel.UserId = 2;
                MagazineModels magazinefind;
                HttpResponseMessage _httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines/" + id).Result;
                magazinefind = _httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;

                if (magazinefind != null)
                {

                    MagazineModels magazine;
                    magazinefind.Description = _magazineViewModel.Description;
                    magazinefind.Favorite = _magazineViewModel.Favorite;
                    magazinefind.Scope = _magazineViewModel.Scope;
                    HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.PutAsJsonAsync("Magazines/" + magazinefind.Id, magazinefind).Result;
                    magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Magazine/Delete/5
        public ActionResult Delete(int id)
        {
            MagazineModels magazine;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines/" + id.ToString()).Result;
            magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;
            return View(magazine);
        }

        // POST: Magazine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MagazineModels _magazine)
        {
            try
            {
                MagazineModels magazine;
                HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.DeleteAsync("Magazines/" + id).Result;
                magazine = httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
