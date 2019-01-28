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
    public class OpgaveController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Opgave
        public ActionResult Index()
        {
            var opgave = db.Opgave.Include(o => o.Kenniselement);
            return View(opgave.ToList());
        }

        // GET: Opgave/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opgave opgave = db.Opgave.Find(id);
            if (opgave == null)
            {
                return HttpNotFound();
            }
            return View(opgave);
        }

        // GET: Opgave/Create
        public ActionResult Create()
        {
            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud");
            return View();
        }

        // POST: Opgave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,elementID,inhoud,score,typeScore,categorie,antwoorden,openbaar,makerID,aanmaakdatum,laatstGewijzigDoor,datumGewijzigd,isBackup")] Opgave opgave)
        {
            if (ModelState.IsValid)
            {
                db.Opgave.Add(opgave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud", opgave.elementID);
            return View(opgave);
        }

        // GET: Opgave/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opgave opgave = db.Opgave.Find(id);
            if (opgave == null)
            {
                return HttpNotFound();
            }
            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud", opgave.elementID);
            return View(opgave);
        }

        // POST: Opgave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,elementID,inhoud,score,typeScore,categorie,antwoorden,openbaar,makerID,aanmaakdatum,laatstGewijzigDoor,datumGewijzigd,isBackup")] Opgave opgave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opgave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud", opgave.elementID);
            return View(opgave);
        }

        // GET: Opgave/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opgave opgave = db.Opgave.Find(id);
            if (opgave == null)
            {
                return HttpNotFound();
            }
            return View(opgave);
        }

        // POST: Opgave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opgave opgave = db.Opgave.Find(id);
            db.Opgave.Remove(opgave);
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
