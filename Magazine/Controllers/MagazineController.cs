using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class MagazineController : Controller
    {
        // GET: Magazine
        public ActionResult Index()
        {
            IEnumerable<MagazineModels> listMagazines;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines").Result;
            listMagazines = httpResponseMessage.Content.ReadAsAsync<IEnumerable<MagazineModels>>().Result;
            return View(listMagazines);
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

        // POST: Magazine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MagazineModels _magazine)
        {
            try
            {
                _magazine.UserId = 2;
                MagazineModels magazinefind;
                HttpResponseMessage _httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines/" + id).Result;
                magazinefind = _httpResponseMessage.Content.ReadAsAsync<MagazineModels>().Result;

                if (magazinefind != null)
                {

                    MagazineModels magazine;
                    HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.PutAsJsonAsync("Magazines/" + _magazine.Id, _magazine).Result;
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
