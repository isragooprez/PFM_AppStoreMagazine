using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using API.Services;

namespace API.Services
{
    public class MagazinesServices : ApiController
    {
        private MagazinesDBEntities db = new MagazinesDBEntities();


        public IQueryable<Magazine> GetMagazines()
        {
            return db.Magazines;
        }
        public Magazine GetMagazine(int id)
        {
            return db.Magazines.Find(id);
        }

        public void PutMagazine(int id, Magazine magazine)
        {
            db.Entry(magazine).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void PostMagazine(Magazine magazine)
        {
            db.Magazines.Add(magazine);
            db.SaveChanges();
        }

        public Magazine DeleteMagazine(Magazine magazine)
        {
            Magazine _magazine = db.Magazines.Remove(magazine);
            db.SaveChanges();
            return magazine;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool MagazineExists(int id)
        {
            return db.Magazines.Count(e => e.Id == id) > 0;
        }
    }
}