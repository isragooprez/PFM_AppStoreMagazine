using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Magazine.Content.Utils;

namespace Magazine.Controllers
{
    [Authorize]
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
                    IEnumerable<MagazineModels> magazineStoreModelsExistStored;
                    var isExisteProdVirtualCar = (from mgz in _magazineStoreVirtualModels where mgz.Url == url select mgz).Any();

                    HttpResponseMessage httpResponseMessage;
                    httpResponseMessage = GlobalVarApi.WebApiClient.GetAsync("Magazines/Magazine/" + Utils.UrlEnCode(url.Replace(ConfigurationManager.AppSettings.Get("BASE_URL"), string.Empty)) + "/" + User.Identity.GetUserId()).Result;
                    magazineStoreModelsExistStored = httpResponseMessage.Content.ReadAsAsync<IEnumerable<MagazineModels>>().Result;

                    if (isExisteProdVirtualCar == true)
                    {
                        ViewBag.MessageExist = Resources.Resource.MnsMgzStoreExist;
                        TempData["ErrorMessage"] = Resources.Resource.MnsMgzStoreExist;
                    }
                    else if (magazineStoreModelsExistStored != null && magazineStoreModelsExistStored.Count() > 0)
                    {
                        ViewBag.MessageExist = Resources.Resource.MnsMgzStoreExist;
                        TempData["ErrorMessage"] = Resources.Resource.MnsMgzStoreExistFavorite;

                    }
                    else
                    {
                        string cleanURL = url.Trim().Replace(ConfigurationManager.AppSettings.Get("BASE_URL"), string.Empty).Replace('&', ',');
                        MagazineStoreModels magazineModels;
                        HttpResponseMessage httpResponseMessages;
                        httpResponseMessages = GlobalVarApi.WebApiClient.GetAsync("Nsoup/GetDataMagazine/" + cleanURL).Result;
                        magazineModels = httpResponseMessages.Content.ReadAsAsync<MagazineStoreModels>().Result;
                        magazineModels.UserId = User.Identity.GetUserId();
                        var idd = User.Identity.GetUserId();
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
                    _magazine.Favorite = false;
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
