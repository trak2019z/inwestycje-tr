using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using inwestycje.Models;

namespace inwestycje.Controllers
{
    public class sklepiesController : Controller
    {
        private sklepyEntities db = new sklepyEntities();

        // GET: sklepies
        public ActionResult Index()
        {
            return View(db.sklepy.ToList());
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
