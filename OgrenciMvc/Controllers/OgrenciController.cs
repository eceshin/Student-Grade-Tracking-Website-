using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OgrenciMvc.Controllers;
using OgrenciMvc.Models.EntityFramework;

namespace OgrenciMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities db= new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var Ogrenciler = db.TBLOGRENCILER.ToList();
            return View(Ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> degerler=(from i in db.TBLKULUPLER.ToList()
                                           select new SelectListItem
                                           {
                                               Text=i.KULUPAD,
                                               Value=i.KULUPID.ToString(),
                                           }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCILER p3)
        {
            var klp=db.TBLKULUPLER.Where(m=>m.KULUPID==p3.TBLKULUPLER.KULUPID).FirstOrDefault();
            p3.TBLKULUPLER = klp;
            db.TBLOGRENCILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Find(id);
            db.TBLOGRENCILER.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OgrenciGetir(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKULUPLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("OgrenciGetir",ogrenci);
        }
        public ActionResult Guncelle(TBLOGRENCILER o)
        {
            var ogr = db.TBLOGRENCILER.Find(o.OGRENCIID);
            ogr.OGRAD = o.OGRAD;
            ogr.OGRSOYADI = o.OGRSOYADI;
            ogr.OGRCINSIYET = o.OGRCINSIYET;
            ogr.OGRFOTOGRAF = o.OGRFOTOGRAF;
            var klp = db.TBLKULUPLER.Where(m => m.KULUPID == o.TBLKULUPLER.KULUPID).FirstOrDefault();
           ogr.OGRKULUP= klp.KULUPID;
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");
        }
    }
}