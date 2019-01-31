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
    public class ToetsController : Controller
    {
        private ToetsgeneratorModel db = new ToetsgeneratorModel();

        // GET: Toets
        [HttpGet]
        public ActionResult Index(string search)
        {
            if (search != null)
            {
                var toets = db.Toets
                                .Include(t => t.Toetsmatrijs)
                                .Where(x => x.categorie == search.ToString());
                                



                return View(toets);
            }
            else
            {
                var toets = db.Toets.Include(t => t.Toetsmatrijs);
                return View(toets.ToList());
            }
            
        }

        // GET: Toets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toets toets = db.Toets
                            .Include(t => t.ToetsOpgave)
                            .Where(x => x.ID == id)
                            .First();


            if (toets == null)
            {
                return HttpNotFound();
            }
            return View(toets);
        }
        //Goedkeuren van de toets
        public ActionResult Goedkeuren(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toets toets = db.Toets.Find(id);
            if (toets == null)
            {
                return HttpNotFound();
            }
            toets.status = "Bevestigd";
            db.Entry(toets).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details/" + id);

        }

        // GET: Toets/Create
        public ActionResult Create()
        {
            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam");
            return View();
        }

        // POST: Toets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,matrijsID,categorie,studiejaar,blokperiode,toetsgelegenheid,tijdsduur,schrapformulier,examinatoren,maker,aanmaakDatum,laatstGewijzigdDoor,datumGewijzigd,status")] Toets toets)
        {
            if (ModelState.IsValid)
            {
                db.Toets.Add(toets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam", toets.matrijsID);
            return View(toets);
        }

        // GET: Toets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toets toets = db.Toets.Find(id);
            if (toets == null)
            {
                return HttpNotFound();
            }
            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam", toets.matrijsID);
            return View(toets);
        }

        // POST: Toets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,matrijsID,categorie,studiejaar,blokperiode,toetsgelegenheid,tijdsduur,schrapformulier,examinatoren,maker,aanmaakDatum,laatstGewijzigdDoor,datumGewijzigd,status")] Toets toets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.matrijsID = new SelectList(db.Toetsmatrijs, "ID", "moduleNaam", toets.matrijsID);
            return View(toets);
        }

        // GET: Toets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toets toets = db.Toets.Find(id);
            if (toets == null)
            {
                return HttpNotFound();
            }
            return View(toets);
        }

        // POST: Toets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Toets toets = db.Toets.Find(id);
            db.Toets.Remove(toets);
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
