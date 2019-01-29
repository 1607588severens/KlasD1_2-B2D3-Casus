using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D1_2_B2D3_Casus_Toetsgenerator.Models;

namespace D1_2_B2D3_Casus_Toetsgenerator.Controllers
{
    public class KenniselementController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Kenniselement
        public ActionResult Index()
        {
            var kenniselement = db.Kenniselement.Include(k => k.Toetsmatrijs);
            return View(kenniselement.ToList());
        }

        // GET: Kenniselement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kenniselement kenniselement = db.Kenniselement.Find(id);
            if (kenniselement == null)
            {
                return HttpNotFound();
            }
            return View(kenniselement);
        }

        // GET: Kenniselement/Create
        public ActionResult Create()
        {
            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam");
            return View();
        }

        // POST: Kenniselement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,matrijsID,inhoud,aantalReproductie,aantalBegrip,aantalToepassing")] Kenniselement kenniselement)
        {
            if (ModelState.IsValid)
            {
                db.Kenniselement.Add(kenniselement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam", kenniselement.matrijsID);
            return View(kenniselement);
        }

        // GET: Kenniselement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kenniselement kenniselement = db.Kenniselement.Find(id);
            if (kenniselement == null)
            {
                return HttpNotFound();
            }
            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam", kenniselement.matrijsID);
            return View(kenniselement);
        }

        // POST: Kenniselement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,matrijsID,inhoud,aantalReproductie,aantalBegrip,aantalToepassing")] Kenniselement kenniselement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kenniselement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam", kenniselement.matrijsID);
            return View(kenniselement);
        }

        // GET: Kenniselement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kenniselement kenniselement = db.Kenniselement.Find(id);
            if (kenniselement == null)
            {
                return HttpNotFound();
            }
            return View(kenniselement);
        }

        // POST: Kenniselement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kenniselement kenniselement = db.Kenniselement.Find(id);
            db.Kenniselement.Remove(kenniselement);
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
