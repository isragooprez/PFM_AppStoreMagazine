using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class MagazinesDiaryController : Controller
    {
        // GET: MagazineDiary
        public ActionResult Index()
        {

            return View();
        }


        // GET: MagazineDiary/ListNsoupFilter/filter
        public ActionResult ListNsoupFilter(string filter)
        {
            NsoupMagazineModels nsoupModels;
            HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/" + filter).Result;
            nsoupModels = httpResponseMessage.Content.ReadAsAsync<NsoupMagazineModels>().Result;
            return View(nsoupModels);
        }

        // GET: MagazinesDiary/AddDiary/5
        public ActionResult AddDiary(string url, MagazinesVirtualModels _magazineDiaryModels)
        {
            try
            {
                if (url == null && _magazineDiaryModels.Count() == 0)
                    ViewBag.Message = "There are no added magazines.";
                else if (url==null && _magazineDiaryModels.Count()>0 )
                    return View(_magazineDiaryModels);
                else
                {
                    var isExisteProdVirtualCar = (from mgz in _magazineDiaryModels where mgz.Url == url select mgz).Any();
                    if (isExisteProdVirtualCar == true)
                    {
                        ViewBag.MessageExist = "La revista ya se a sido Agendada.";
                    }
                    else
                    {
                        string cleanURL = url.Trim().Replace(ConfigurationManager.AppSettings.Get("BASE_URL"), string.Empty).Replace('&', ',');
                        MagazineStoreModels magazineModels;
                        HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/GetDataMagazine/" + cleanURL).Result;
                        magazineModels = httpResponseMessage.Content.ReadAsAsync<MagazineStoreModels>().Result;
                        //
                        // TODO OJO FALTA cont5rolar auntomatico
                        //
                        magazineModels.UserId = 2;
                        _magazineDiaryModels.Add(magazineModels);
                    }
                }
                return View(_magazineDiaryModels);
            }
            catch
            {
                return RedirectToAction("Index", "Home", _magazineDiaryModels);
            }
        }

        public ActionResult StoreMagazines(MagazinesVirtualModels _magazineDiaryModels)
        {
            try
            {
                MagazineStoreModels magazine_created = new MagazineStoreModels();
                foreach (var _magazine in _magazineDiaryModels)
                {
                    HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.PostAsJsonAsync("Magazines", _magazine).Result;
                    magazine_created = httpResponseMessage.Content.ReadAsAsync<MagazineStoreModels>().Result;
                }
                CleanStoreVirtualMagazines(_magazineDiaryModels);
                _magazineDiaryModels = null;
                if (magazine_created != null) return RedirectToAction("Index", "Home"); else return RedirectToAction("AddDiary", "MagazinesDiary");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: MagazineDiary/Details/5
        public ActionResult Details(string url, MagazinesVirtualModels _magazineDiaryModels)
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
        public ActionResult Delete(string url, MagazinesVirtualModels _magazineDiaryModels)
        {
            MagazinesVirtualModels aux_magazineStoreModels = new MagazinesVirtualModels();

            var isExisteProdVirtualCar = (from mgz in _magazineDiaryModels where mgz.Url == url select mgz).Any();
            if (isExisteProdVirtualCar == true)
            {
                var mgzvirtualMagazine = (from mgz in _magazineDiaryModels where mgz.Url == url select mgz).First();
                foreach (MagazineStoreModels mgzDiary in _magazineDiaryModels)
                {
                    if (mgzvirtualMagazine.Url == mgzDiary.Url)
                        aux_magazineStoreModels.Add(mgzDiary);
                }

                foreach (MagazineStoreModels aux_mgzDiary in aux_magazineStoreModels)
                {
                    //if (mgzvirtualMagazine.Url == aux_mgzDiary.Url)
                        _magazineDiaryModels.Remove(aux_mgzDiary);
                }

            }
            return RedirectToAction("AddDiary", _magazineDiaryModels);
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

        public void CleanStoreVirtualMagazines(MagazinesVirtualModels _magazineDiaryModels)
        {
            _magazineDiaryModels.Clear();
        }
    }
}
