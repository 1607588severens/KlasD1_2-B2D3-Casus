using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D1_2_B2D3_Casus_Toetsgenerator.Models;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace D1_2_B2D3_Casus_Toetsgenerator.Controllers
{
    public class ToetsmatrijsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Toetsmatrijs
        public ActionResult Index()
        {
            return View(db.Toetsmatrijs.ToList());
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Convert(int? id)
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

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            _Worksheet oSheet;

            //Start Excel and get Application object.
            oXL = new Application();
            oXL.Visible = true;

            //verkrijgen nieuw werkboek.
            oWB = (oXL.Workbooks.Add(Missing.Value));
            oSheet = oWB.ActiveSheet;

            //Vul toetsmatrijs met gegevens uit de database
            int row = 9;
            foreach (var kenniselement in toetsmatrijs.Kenniselement)
            {
                oSheet.Cells[row, 1] = kenniselement.inhoud.ToString();

                for (int col = 2; col < 5; col++)
                {
                    if (col == 2)
                    {
                        oSheet.Cells[row, col] = kenniselement.aantalReproductie.ToString();
                    }
                    else if (col == 3)
                    {
                        oSheet.Cells[row, col] = kenniselement.aantalBegrip.ToString();
                    }
                    else if (col == 4)
                    {
                        oSheet.Cells[row, col] = kenniselement.aantalToepassing.ToString();
                    }

                }
                row += 1;
            }

            //Vul toetsmatrijs met gegevens uit de database
            row = 23;
            foreach (var kenniselement in toetsmatrijs.Kenniselement)
            {
                oSheet.Cells[row, 1] = kenniselement.inhoud.ToString();

                for (int col = 2; col < 5; col++)
                {
                    string datastr = "";
                    if (col == 2)
                    {
                        foreach (var opgave in kenniselement.Opgave)
                        {
                            if (opgave.typeScore == "Reproductie")
                            {
                                if (datastr.Length < 1)
                                {
                                    datastr = opgave.ID.ToString();
                                }
                                else
                                {
                                    datastr = datastr + ", " + opgave.ID.ToString();
                                }
                            }
                        }
                        oSheet.Cells[row, col] = datastr;
                    }
                    else if (col == 3)
                    {
                        foreach (var opgave in kenniselement.Opgave)
                        {
                            if (opgave.typeScore == "Begrip")
                            {
                                if (datastr.Length < 1)
                                {
                                    datastr = opgave.ID.ToString();
                                }
                                else
                                {
                                    datastr = datastr + ", " + opgave.ID.ToString();
                                }
                            }
                        }
                        oSheet.Cells[row, col] = datastr;
                    }
                    else if (col == 4)
                    {
                        foreach (var opgave in kenniselement.Opgave)
                        {
                            if (opgave.typeScore == "Toepassing")
                            {
                                if (datastr.Length < 1)
                                {
                                    datastr = opgave.ID.ToString();
                                }
                                else
                                {
                                    datastr = datastr + ", " + opgave.ID.ToString();
                                }
                            }
                        }
                        oSheet.Cells[row, col] = datastr;
                    }

                }
                row += 1;
            }

            //Standaard cell gegevens volgens toetsmatrijs
            oSheet.Cells[1, 1] = "Toetsniveau";
            oSheet.Cells[1, 2] = "Reproductie";
            oSheet.Cells[1, 3] = "Begrip";
            oSheet.Cells[1, 4] = "Toepassing";
            oSheet.Cells[2, 1] = "Vraagtype";
            oSheet.Cells[2, 2] = "Kennisvraag";
            oSheet.Cells[2, 3] = "Inzichtvraag";
            oSheet.Cells[2, 4] = "Toepassingsvraag";
            oSheet.Cells[3, 1] = "Streefverdeling voor PI 1";
            oSheet.Cells[3, 2] = "60%";
            oSheet.Cells[3, 3] = "40%";
            oSheet.Cells[3, 4] = "0%";
            oSheet.Cells[4, 1] = "Streefverdeling voor PI 2";
            oSheet.Cells[4, 2] = "40%";
            oSheet.Cells[4, 3] = "60%";
            oSheet.Cells[4, 4] = "0%";
            oSheet.Cells[5, 1] = "Streefverdeling voor PI 3";
            oSheet.Cells[5, 2] = "20%";
            oSheet.Cells[5, 3] = "30%";
            oSheet.Cells[5, 4] = "50%";
            oSheet.Cells[7, 1] = toetsmatrijs.moduleCode + toetsmatrijs.prestatieIndicator.ToString();
            oSheet.Cells[7, 2] = "Aantal punten";
            oSheet.Cells[8, 1] = "Kenniselementen";
            oSheet.Cells[8, 2] = "Reproductie";
            oSheet.Cells[8, 3] = "Begrip";
            oSheet.Cells[8, 4] = "Toepassing";
            oSheet.Cells[8, 5] = "Totaal";
            oSheet.Cells[8, 6] = "Percentage";
            oSheet.Cells[17, 1] = "Totaal";
            oSheet.Cells[18, 1] = "Percentage";
            oSheet.Cells[21, 1] = "Toetsperiode";
            oSheet.Cells[21, 2] = "Vraagnummers";
            oSheet.Cells[22, 1] = "Kenniselementen";
            oSheet.Cells[22, 2] = "Reproductie";
            oSheet.Cells[22, 3] = "Begrip";
            oSheet.Cells[22, 4] = "Toepassing";

            //Standaard style van toetsmatrijs
            oSheet.Columns.ColumnWidth = 25;
            oSheet.get_Range("B1", "F30").Columns.ColumnWidth = 14;
            oSheet.get_Range("A1", "A5").Font.Bold = true;
            oSheet.get_Range("A7", "D7").Font.Bold = true;
            oSheet.get_Range("A7", "F18").Borders.LineStyle = XlLineStyle.xlContinuous;
            oSheet.get_Range("A21", "D30").Borders.LineStyle = XlLineStyle.xlContinuous;
            oSheet.get_Range("A8", "F8").Font.Bold = true;
            oSheet.get_Range("A17", "A18").Font.Bold = true;
            oSheet.get_Range("A21", "B21").Font.Bold = true;
            oSheet.get_Range("A21", "A22").Font.Bold = true;
            oSheet.get_Range("A22", "D22").Font.Bold = true;
            oSheet.get_Range("B7", "D7").MergeCells = true;
            oSheet.get_Range("B7", "D7").HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oSheet.get_Range("B21", "D21").MergeCells = true;
            oSheet.get_Range("B21", "D21").HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oSheet.get_Range("A1", "D5").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 204));
            oSheet.get_Range("B9", "D16").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 153));
            oSheet.get_Range("F9", "F17").NumberFormat = "0%";
            oSheet.get_Range("B18", "E18").NumberFormat = "0%";

            //Standaard formules toetsmatrijs
            oSheet.get_Range("F9", "F16").Formula = "=E9 / E$17";
            oSheet.get_Range("E9", "E16").Formula = "=SUM(B9:D9)";
            oSheet.Cells[17, 5].Formula = "=SUM(E9:E16)";
            oSheet.Cells[17, 6].Formula = "=E17 / E$17";
            oSheet.Cells[18, 5].Formula = "=E17 / E$17 ";
            oSheet.Cells[17, 2].Formula = "=SUM(B9:B16)";
            oSheet.Cells[17, 3].Formula = "=SUM(C9:C16)";
            oSheet.Cells[17, 4].Formula = "=SUM(D9:D16)";
            oSheet.get_Range("B18", "D18").Formula = "=B17 / $E17";



            return RedirectToAction("Index");
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
