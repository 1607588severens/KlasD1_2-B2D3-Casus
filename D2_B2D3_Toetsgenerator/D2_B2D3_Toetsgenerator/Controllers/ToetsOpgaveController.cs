using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D2_B2D3_Toetsgenerator.Models;

namespace D2_B2D3_Toetsgenerator.Controllers
{
    public class ToetsOpgaveController : Controller
    {
        private ToetsgeneratorModel db = new ToetsgeneratorModel();

        // GET: ToetsOpgave
        public ActionResult Index()
        {
            var toetsOpgave = db.ToetsOpgave.Include(t => t.Opgave).Include(t => t.Toets);
            return View(toetsOpgave.ToList());
        }

        // GET: ToetsOpgave/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToetsOpgave toetsOpgave = db.ToetsOpgave.Find(id);
            if (toetsOpgave == null)
            {
                return HttpNotFound();
            }
            return View(toetsOpgave);
        }

        // GET: ToetsOpgave/Create
        public ActionResult Create()
        {
            ViewBag.opgaveID = new SelectList(db.Opgave, "ID", "inhoud");
            ViewBag.toetsID = new SelectList(db.Toets, "ID", "categorie");
            return View();
        }

        // POST: ToetsOpgave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,toetsID,opgaveID")] ToetsOpgave toetsOpgave)
        {
            if (ModelState.IsValid)
            {
                db.ToetsOpgave.Add(toetsOpgave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.opgaveID = new SelectList(db.Opgave, "ID", "inhoud", toetsOpgave.opgaveID);
            ViewBag.toetsID = new SelectList(db.Toets, "ID", "categorie", toetsOpgave.toetsID);
            return View(toetsOpgave);
        }

        // GET: ToetsOpgave/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToetsOpgave toetsOpgave = db.ToetsOpgave.Find(id);
            if (toetsOpgave == null)
            {
                return HttpNotFound();
            }
            ViewBag.opgaveID = new SelectList(db.Opgave, "ID", "inhoud", toetsOpgave.opgaveID);
            ViewBag.toetsID = new SelectList(db.Toets, "ID", "categorie", toetsOpgave.toetsID);
            return View(toetsOpgave);
        }

        // POST: ToetsOpgave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,toetsID,opgaveID")] ToetsOpgave toetsOpgave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toetsOpgave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.opgaveID = new SelectList(db.Opgave, "ID", "inhoud", toetsOpgave.opgaveID);
            ViewBag.toetsID = new SelectList(db.Toets, "ID", "categorie", toetsOpgave.toetsID);
            return View(toetsOpgave);
        }

        // GET: ToetsOpgave/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToetsOpgave toetsOpgave = db.ToetsOpgave.Find(id);
            if (toetsOpgave == null)
            {
                return HttpNotFound();
            }
            return View(toetsOpgave);
        }

        // POST: ToetsOpgave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToetsOpgave toetsOpgave = db.ToetsOpgave.Find(id);
            db.ToetsOpgave.Remove(toetsOpgave);
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
