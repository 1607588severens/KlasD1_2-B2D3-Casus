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
    public class ToetsmatrijsController : Controller
    {
        private ToetsgeneratorModel db = new ToetsgeneratorModel();

        // GET: Toetsmatrijs
        public ActionResult Index()
        {
            return View(db.Toetsmatrijs.ToList());
        }

        // GET: Toetsmatrijs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toetsmatrijs toetsmatrijs = db.Toetsmatrijs.Find(id);
            if (toetsmatrijs == null)
            {
                return HttpNotFound();
            }
            return View(toetsmatrijs);
        }

        // GET: Toetsmatrijs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Toetsmatrijs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,moduleNaam,moduleCode,makerID,aanmaakdatum,laatstGewijzigdDoor,datumGewijzigd,prestatieIndicator")] Toetsmatrijs toetsmatrijs)
        {
            if (ModelState.IsValid)
            {
                db.Toetsmatrijs.Add(toetsmatrijs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toetsmatrijs);
        }

        // GET: Toetsmatrijs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toetsmatrijs toetsmatrijs = db.Toetsmatrijs.Find(id);
            if (toetsmatrijs == null)
            {
                return HttpNotFound();
            }
            return View(toetsmatrijs);
        }

        // POST: Toetsmatrijs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,moduleNaam,moduleCode,makerID,aanmaakdatum,laatstGewijzigdDoor,datumGewijzigd,prestatieIndicator")] Toetsmatrijs toetsmatrijs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toetsmatrijs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toetsmatrijs);
        }

        // GET: Toetsmatrijs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toetsmatrijs toetsmatrijs = db.Toetsmatrijs.Find(id);
            if (toetsmatrijs == null)
            {
                return HttpNotFound();
            }
            return View(toetsmatrijs);
        }

        // POST: Toetsmatrijs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Toetsmatrijs toetsmatrijs = db.Toetsmatrijs.Find(id);
            db.Toetsmatrijs.Remove(toetsmatrijs);
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
