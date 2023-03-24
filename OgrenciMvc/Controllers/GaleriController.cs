using OgrenciMvc.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OgrenciMvc.Models;

namespace OgrenciMvc.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri

        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var Ogrenciler = db.TBLOGRENCILER.ToList();
            return View(Ogrenciler);
        }

        [HttpPost]
        public ActionResult Yukle(TBLOGRENCILER p3)
        {
            var klp = db.TBLKULUPLER.Where(m => m.KULUPID == p3.TBLKULUPLER.KULUPID).FirstOrDefault();
            p3.TBLKULUPLER = klp;
            db.TBLOGRENCILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}