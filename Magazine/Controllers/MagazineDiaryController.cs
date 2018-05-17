using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class MagazineDiaryController : Controller
    {
        // GET: MagazineDiary
        public ActionResult Index()
        {

            return View();
        }


        // GET: MagazineDiary/ListNsoupFilter/filter
        public ActionResult ListNsoupFilter(string filter = " ")
        {
            NsoupMagazineModels nsoupModels;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/" + filter).Result;
            nsoupModels = httpResponseMessage.Content.ReadAsAsync<NsoupMagazineModels>().Result;
            return View(nsoupModels);
        }
        // GET: MagazineDiary/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MagazineDiary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MagazineDiary/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MagazineDiary/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MagazineDiary/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MagazineDiary/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MagazineDiary/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
