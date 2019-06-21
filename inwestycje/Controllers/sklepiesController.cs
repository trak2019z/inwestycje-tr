using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using inwestycje.Models;
using Rotativa;
using Rotativa.MVC;

namespace inwestycje.Controllers
{
    public class sklepiesController : Controller
    {
        private sklepyEntities db = new sklepyEntities();

        // GET: sklepies
        [Authorize]
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MiastoSortParm = sortOrder == "Miasto" ? "miasto_desc" : "Miasto";
            ViewBag.NazwaGaleriiSortParm = sortOrder == "NazwaGalerii" ? "nazwagalerii_desc" : "NazwaGalerii";
            ViewBag.MetrazSortParm = sortOrder == "Metraz" ? "metraz_desc" : "Metraz";
            ViewBag.BrygadzistaSortParm = sortOrder == "Brygadzista" ? "brygadzista_desc" : "Brygadzista";
            ViewBag.LokalSortParm = sortOrder == "Lokal" ? "lokal_desc" : "Lokal";
            var sklepy = from s in db.sklepy
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sklepy = sklepy.Where(s => s.Miasto.ToUpper().Contains(searchString.ToUpper())
                                        || s.NazwaGalerii.ToUpper().Contains(searchString.ToUpper())
                                        || s.Brygadzista.ToUpper().Contains(searchString.ToUpper())
                                        || s.Lokal.ToUpper().Contains(searchString.ToUpper()));

            }
                switch (sortOrder)
            {
                case "name_desc":
                    sklepy = sklepy.OrderByDescending(s => s.Lokal);
                    break;
                case "Lokal":
                    sklepy = sklepy.OrderBy(s => s.Lokal);
                    break;
                case "lokal_desc":
                    sklepy = sklepy.OrderByDescending(s => s.Lokal);
                    break;

                case "Miasto":
                    sklepy = sklepy.OrderBy(s => s.Miasto);
                    break;
                case "miasto_desc":
                    sklepy = sklepy.OrderByDescending(s => s.Miasto);
                    break;
                case "NazwaGalerii":
                    sklepy = sklepy.OrderBy(s => s.NazwaGalerii);
                    break;
                case "nazwagalerii_desc":
                    sklepy = sklepy.OrderByDescending(s => s.NazwaGalerii);
                    break;
                case "Metraz":
                    sklepy = sklepy.OrderBy(s => s.Metraz);
                    break;
                case "metraz_desc":
                    sklepy = sklepy.OrderByDescending(s => s.Metraz);                              
                    break;
                case "Brygadzista":
                    sklepy = sklepy.OrderBy(s => s.Brygadzista);
                    break;
                case "brygadzista_desc":
                    sklepy = sklepy.OrderByDescending(s => s.Brygadzista);
                    break;
                default:
                    
                    break;
            }
            return View(sklepy.ToList());
        }

        // GET: sklepies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sklepy sklepy = db.sklepy.Find(id);
            if (sklepy == null)
            {
                return HttpNotFound();
            }
            return View(sklepy);
        }

        // GET: sklepies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sklepies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lokal,Miasto,NazwaGalerii,Metraz,Brygadzista")] sklepy sklepy)
        {
            if (ModelState.IsValid)
            {
                db.sklepy.Add(sklepy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sklepy);
        }

        // GET: sklepies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sklepy sklepy = db.sklepy.Find(id);
            if (sklepy == null)
            {
                return HttpNotFound();
            }
            return View(sklepy);
        }

        // POST: sklepies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Lokal,Miasto,NazwaGalerii,Metraz,Brygadzista")] sklepy sklepy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sklepy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sklepy);
        }

        // GET: sklepies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sklepy sklepy = db.sklepy.Find(id);
            if (sklepy == null)
            {
                return HttpNotFound();
            }
            return View(sklepy);
        }

        // POST: sklepies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sklepy sklepy = db.sklepy.Find(id);
            db.sklepy.Remove(sklepy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // drukowanie do PDF

        public ActionResult DrukujPdf()
        {
            var AllSklepy = db.sklepy.ToList();
            return View(AllSklepy);
        }

        public ActionResult PrintAll()
        {
            var q = new ActionAsPdf("DrukujPdf");
            return q;

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
