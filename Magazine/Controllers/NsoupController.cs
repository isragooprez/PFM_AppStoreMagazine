using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class NsoupController : Controller
    {
        // GET: Nsoup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Nsoup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Nsoup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nsoup/Create
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

        // GET: Nsoup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Nsoup/Edit/5
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

        // GET: Nsoup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Nsoup/Delete/5
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
