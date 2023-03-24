using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciMvc.Controllers;
using OgrenciMvc.Models.EntityFramework;
using OgrenciMvc.Models;

namespace OgrenciMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db=new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var Notlar = db.TBLNOTLAR.ToList();
            return View(Notlar);
        }

        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR tbn)
        {
            db.TBLNOTLAR.Add(tbn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int id)
        {
            var notlar = db.TBLNOTLAR.Find(id);
            return View("NotGetir",notlar);
        }

        [HttpPost]
        public ActionResult NotGetir(Class1 model,TBLNOTLAR n, int SINAV1 = 0, int SINAV2 = 0, int SINAV3 = 0, int PROJE = 0)
        {
            if (model.islem == "HESAPLA")
            {

                int ORTALAMA = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ORTALAMA;
            }

            if (model.islem == "NOTGUNCELLE")
            {

                var not = db.TBLNOTLAR.Find(n.NOTID);
                not.SINAV1 = n.SINAV1;
                not.SINAV2 = n.SINAV2;
                not.SINAV3 = n.SINAV3;
                not.PROJE = n.PROJE;
                not.ORTALAMA = n.ORTALAMA;
                not.DURUM = n.DURUM;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }
                return View();
        }
       
    }
}