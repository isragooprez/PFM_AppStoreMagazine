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
    public class MagazinesStoreController : Controller
    {

        // GET: MagazinesDiary/AddDiary/5
        public ActionResult AddDiary(string url, MagazinesVirtualModels _magazineStoreVirtualModels)
        {
            try
            {
                if (url == null && _magazineStoreVirtualModels.Count() == 0)
                    ViewBag.Message = Resources.Resource.MsnMgzNotStored;
                else if (url == null && _magazineStoreVirtualModels.Count() > 0)
                {
                    ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
                    return View(_magazineStoreVirtualModels);
                }
                else
                {
                    var isExisteProdVirtualCar = (from mgz in _magazineStoreVirtualModels where mgz.Url == url select mgz).Any();
                    if (isExisteProdVirtualCar == true)
                    {
                        ViewBag.MessageExist = Resources.Resource.MnsMgzStoreExist;
                        TempData["ErrorMessage"] = Resources.Resource.MnsMgzStoreExist;
                    }
                    else
                    {
                        string cleanURL = url.Trim().Replace(ConfigurationManager.AppSettings.Get("BASE_URL"), string.Empty).Replace('&', ',');
                        MagazineStoreModels magazineModels;
                        HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Nsoup/GetDataMagazine/" + cleanURL).Result;
                        magazineModels = httpResponseMessage.Content.ReadAsAsync<MagazineStoreModels>().Result;
                        //
                        // TODO OJO FALTA cont5rolar auntomatico 
                        //USUARIO 
                        //FABORITO y FEHCA desde el MODELO
                        //

                        magazineModels.UserId = 2;
                        _magazineStoreVirtualModels.Add(magazineModels);
                        TempData["SuccessMessage"] = Resources.Resource.MnsMgzSucessStoreVirtual;
                    }
                }
                ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
                return RedirectToAction("Index", "Home", _magazineStoreVirtualModels);
            }
            catch
            {
                return RedirectToAction("Index", "Home", _magazineStoreVirtualModels);
            }
        }

        public ActionResult StoreMagazines(MagazinesVirtualModels _magazineDiaryModels)
        {
            try
            {
                MagazineStoreModels magazine_created = new MagazineStoreModels();
                foreach (var _magazine in _magazineDiaryModels)
                {
                    _magazine.Favorite = "F";
                    _magazine.DateIn = DateTime.Now;
                    HttpResponseMessage httpResponseMessage = GlobalVarApi.WebApiClient.PostAsJsonAsync("Magazines", _magazine).Result;
                    magazine_created = httpResponseMessage.Content.ReadAsAsync<MagazineStoreModels>().Result;
                }
                CleanStoreVirtualMagazines(_magazineDiaryModels);
                _magazineDiaryModels = null;
                if (magazine_created != null)
                {
                    TempData["SuccessMessage"] = Resources.Resource.MnsMgzSucessSave;
                    return RedirectToAction("Index", "Magazine");
                }
                else
                {
                    TempData["ErrorMessage"] = Resources.Resource.MsnMgzErrorStoredVirtual;
                    return RedirectToAction("Index", "Home");
                }
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
        public ActionResult Delete(string url, MagazinesVirtualModels _magazineStoreVirtualModels)
        {
            MagazinesVirtualModels aux_magazineStoreModels = new MagazinesVirtualModels();

            var isExisteProdVirtualCar = (from mgz in _magazineStoreVirtualModels where mgz.Url == url select mgz).Any();
            if (isExisteProdVirtualCar == true)
            {
                var mgzvirtualMagazine = (from mgz in _magazineStoreVirtualModels where mgz.Url == url select mgz).First();
                foreach (MagazineStoreModels mgzDiary in _magazineStoreVirtualModels)
                {
                    if (mgzvirtualMagazine.Url == mgzDiary.Url)
                        aux_magazineStoreModels.Add(mgzDiary);
                }

                foreach (MagazineStoreModels aux_mgzDiary in aux_magazineStoreModels)
                {
                    //if (mgzvirtualMagazine.Url == aux_mgzDiary.Url)
                    _magazineStoreVirtualModels.Remove(aux_mgzDiary);
                }

            }
            ViewBag.TotalMgzVirtual = _magazineStoreVirtualModels.Count();
            return View("AddDiary", _magazineStoreVirtualModels);
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
