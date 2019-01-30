using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D1_2_B2D3_Casus_Toetsgenerator.Models;
using Microsoft.Office.Interop.Word;
using System.Reflection;

namespace D1_2_B2D3_Casus_Toetsgenerator.Controllers
{
    public class ToetsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Toets
        public ActionResult Index()
        {
            var toets = db.Toets.Include(t => t.Toetsmatrijs);
            return View(toets.ToList());
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Convert(int? id, bool isAntwoord)
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

            //Starten nieuwe Word applicatie.
            Application app = new Application();
            object missing = Missing.Value;
            //Aanmaken nieuw document
            Document doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            app.Visible = true;

            //Paragraaf aanmaken met een tabel die 15 rijen heeft en 2 kolommen.
            object EndOfDoc = "\\endofdoc";
            Range rn = doc.Bookmarks.get_Item(ref EndOfDoc).Range;
            Paragraph para = doc.Paragraphs.Add(rn);
            Table tb = para.Range.Tables.Add(rn, 15, 2);

            tb.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            tb.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

            //Tabel invullen met benodigde informatie
            tb.Cell(1, 1).Range.Text = "Modulenaam";
            tb.Cell(1, 2).Range.Text = toets.Toetsmatrijs.moduleNaam;
            tb.Cell(2, 1).Range.Text = "Modulecode";
            tb.Cell(2, 2).Range.Text = toets.Toetsmatrijs.moduleCode;
            tb.Cell(3, 1).Range.Text = "Prestatieindicator";
            tb.Cell(3, 2).Range.Text = toets.Toetsmatrijs.prestatieIndicator.ToString();
            tb.Cell(4, 1).Range.Text = "Studiejaar";
            tb.Cell(4, 2).Range.Text = toets.Toetsmatrijs.aanmaakdatum.Value.Date.Year.ToString();
            tb.Cell(5, 1).Range.Text = "Soort toets";
            tb.Cell(5, 2).Range.Text = toets.categorie;
            tb.Cell(6, 1).Range.Text = "Toetsgelegenheid";
            tb.Cell(6, 2).Range.Text = "gelegenheid " + toets.toetsgelegenheid;
            tb.Cell(7, 1).Range.Text = "Tijdsduur";
            tb.Cell(7, 2).Range.Text = toets.tijdsduur.ToString();
            tb.Cell(8, 1).Range.Text = "Schrapformulier";
            tb.Cell(8, 2).Range.Text = toets.schrapformulier.ToString();
            tb.Cell(9, 1).Range.Text = "Examinatoren";
            tb.Cell(9, 2).Range.Text = toets.examinatoren;

            int total = 0;
            foreach (var item in toets.ToetsOpgave)
            {
                total += item.Opgave.score;
            }

            tb.Cell(10, 1).Range.Text = "Te behalen punten (T)";
            tb.Cell(10, 2).Range.Text = total.ToString();


            int gok = 0;
            foreach (var item in toets.ToetsOpgave)
            {
                if (item.Opgave.categorie == "gesloten vraag")
                {
                    gok += item.Opgave.score / 2;
                }
            }

            tb.Cell(11, 1).Range.Text = "Te gokken punten (G)";
            tb.Cell(11, 2).Range.Text = gok.ToString();
            tb.Cell(12, 1).Range.Text = "Punten voor voldoende(Cesuur)";
            tb.Cell(12, 2).Range.Text = (0.55 * (total - gok) + gok).ToString();
            tb.Cell(13, 1).Range.Text = "Behaalde punten (P)";
            tb.Cell(14, 1).Range.Text = "Studentnaam";
            tb.Cell(15, 1).Range.Text = "Studentnummer";

            
            para.Range.Text += @"Berekening PI-cijfer:  (P - G) / (T - G) * 10.

De examencommissie kan hierop na toetsanalyse een generieke correctie aanbrengen. 
Instructies
- Toegestane hulpmiddelen: schrijfgerei pen met zwarte of blauwe inkt
- Aan het einde van dit formulier is ruimte die als kladpapier gebruikt kan worden(niet losscheuren!)
- Controleer de hele set op volledigheid en schakel de surveillant in indien onderdelen ontbreken
- Maak de opgaven in de aangegeven ruimten op dit formulier
- Lever na afloop alles in bij de surveillant





";

            //Expliciet tekst niet bold maken  
            para.Range.Bold = 0;

            //Variabelen definieren 
            string opgave = "";
            int count = 1;
            string underscore = "_";

            //per vraag nvullen in Word document
            foreach (var item in toets.ToetsOpgave)
            {
                opgave += count.ToString() + ". " + item.Opgave.inhoud + " ; " + item.Opgave.score + " punten te behalen.";
                opgave += underscore;

                //Vraag bold maken
                para.Range.Text += opgave;
                object start = para.Range.Start;
                object end = para.Range.Start + opgave.IndexOf("_");
                Range rnBold = doc.Range(ref start, ref end);
                rnBold.Bold = 1;

                //per vraag lege tabel toevoegen
                Range tbRange = doc.Range(ref end, ref missing);
                Table tbOpgave = para.Range.Tables.Add(tbRange, 1, 1);
                tbOpgave.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                tbOpgave.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                if (isAntwoord)
                {
                    tbOpgave.Cell(1, 1).Range.Text = item.Opgave.antwoorden;
                    tbOpgave.Cell(1, 1).Range.Font.Color = WdColor.wdColorRed;
                }

                para.Range.InsertParagraphAfter();

                opgave = "";
                count++;
            }

            return RedirectToAction("Index");
        }

        // GET: Toets/Details/5
        public ActionResult Details(int? id)
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

        public void WriteWord(bool isAntwoordblad, Document doc, Toets toets) {
            doc.Content.Font.Size = 11;

            doc.Content.Text += "Modulenaam: " + toets.Toetsmatrijs.moduleNaam;
            doc.Content.Text += "Modulecode: " + toets.Toetsmatrijs.moduleCode;
            doc.Content.Text += "Prestatieindicator: " + toets.Toetsmatrijs.prestatieIndicator.ToString();
            doc.Content.Text += "Studiejaar: " + toets.Toetsmatrijs.aanmaakdatum.Value.Date.Year;
            doc.Content.Text += "Soort toets: " + toets.categorie;
            doc.Content.Text += "Toetsgelegenheid: " + "gelegenheid " + toets.toetsgelegenheid;
            doc.Content.Text += "Tijdsduur: " + toets.tijdsduur.ToString();
            doc.Content.Text += "Schrapformulier: " + toets.schrapformulier.ToString();
            doc.Content.Text += "Examinatoren: " + toets.examinatoren;

            int total = 0;
            foreach (var item in toets.ToetsOpgave)
            {
                total += item.Opgave.score;
            }

            doc.Content.Text += "Te behalen punten: " + total.ToString();

            int gok = 0;
            foreach (var item in toets.ToetsOpgave)
            {
                if (item.Opgave.categorie == "gesloten vraag")
                {
                    gok += item.Opgave.score / 2;
                }
            }

            doc.Content.Text += "Te gokken punten: " + gok.ToString();
            doc.Content.Text += "Punten voor voldoende(Cesuur): " + (0.55 * (total - gok) + gok);
            doc.Content.Text += @"Behaalde punten (P): 
Studentnaam: 
Studentnummer:

Berekening PI-cijfer:  (P-G)/(T-G)*10.

De examencommissie kan hierop na toetsanalyse een generieke correctie aanbrengen. 
Instructies 
- Toegestane hulpmiddelen: schrijfgerei pen met zwarte of blauwe inkt 
- Aan het einde van dit formulier is ruimte die als kladpapier gebruikt kan worden(niet losscheuren!) 
- Controleer de hele set op volledigheid en schakel de surveillant in indien onderdelen ontbreken 
- Maak de opgaven in de aangegeven ruimten op dit formulier 
- Lever na afloop alles in bij de surveillant




";
            int count = 1;
            string underscores = "";
            foreach (var item in toets.ToetsOpgave)
            {
                doc.Content.Text += count.ToString() + " " + item.Opgave.inhoud;
                for (int i = 0; i < 164; i++)
                {
                    underscores += "_";
                }
                doc.Content.Text += underscores;
                if (isAntwoordblad) {

                    doc.Content.Text += item.Opgave.antwoorden;
                }
                underscores = "";
                count++;
            }
        }
    }
}
