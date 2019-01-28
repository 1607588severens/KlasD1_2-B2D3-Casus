using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D2_B2D3_Toetsgenerator.Models;
using Microsoft.AspNet.Identity;

namespace D2_B2D3_Toetsgenerator.Controllers
{
    public class OpgaveController : Controller
    {
        private ToetsgeneratorModel db = new ToetsgeneratorModel();

        // GET: Opgaves
        public ActionResult Index()
        {
            ViewBag.userMail = User.Identity.GetUserName();
            var opgave = db.Opgave.Include(o => o.Kenniselement).Where(o => o.isBackup == false);
            return View(opgave.ToList());
        }

        // GET: Opgaves/Details/5
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
            opgave.openbaar.ToString();
            opgave.isBackup.ToString();
            return View(opgave);
        }

        // GET: Opgaves/Create
        public ActionResult Create()
        {
            ViewBag.userMail = User.Identity.GetUserName();
            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud");
            return View();
        }

        // POST: Opgaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,elementID,inhoud,score,antwoorden,openbaar,makerID,aanmaakdatum,laatstGewijzigDoor,datumGewijzigd,categorie,isBackup")] Opgave opgave)
        {
            if (ModelState.IsValid)
            {
                opgave.isBackup = false;
                opgave.openbaar = false;
                db.Opgave.Add(opgave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud", opgave.elementID);
            return View(opgave);
        }

        // GET: Opgaves/Edit/5
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
            ViewBag.userMail = User.Identity.GetUserName();
            ViewBag.elementID = new SelectList(db.Kenniselement, "ID", "inhoud", opgave.elementID);
            return View(opgave);
        }

        // POST: Opgaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,elementID,inhoud,score,antwoorden,openbaar,makerID,aanmaakdatum,laatstGewijzigDoor,datumGewijzigd,categorie,isBackup")] Opgave opgave)
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

        // GET: Opgaves/Delete/5
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

        // POST: Opgaves/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Opgave opgave = db.Opgave.Find(id);
        //    db.Opgave.Remove(opgave);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opgave opgave = db.Opgave.Find(id);
            db.Entry(opgave).State = EntityState.Modified;
            opgave.isBackup = true;
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
