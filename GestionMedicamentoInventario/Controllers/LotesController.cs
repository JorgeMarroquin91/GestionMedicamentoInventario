using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionMedicamentoInventario.Models;
using GestionMedicamentoInventario.Seguridad;

namespace GestionMedicamentoInventario.Controllers
{
    public class LotesController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Lotes
        [CustomAuthorize(Roles = "loteIndex")]
        public ActionResult Index()
        {
            var lote = db.Lote.Include(l => l.Medicamento);
            return View(lote.ToList());
        }

        // GET: Lotes/Details/5
        [CustomAuthorize(Roles = "loteDetails")]
        public ActionResult Details(int? id)
        {
            Medicamento medicamento = new Medicamento();
            var kar = from k in db.Kardex
                      where k.idKardex == id
                      select k;
            int idMedicamento = 0;

            foreach (Kardex kard in kar)
            {
                if (kard.idKardex == id)
                {
                    idMedicamento = kard.idMedicamento;
                    break;
                }
            }

            var med = from m in db.Medicamento
                      where m.idMedicamento == idMedicamento
                      select m;

            foreach (Medicamento medi in med)
            {
                medicamento = medi;
                break;
            }

            ViewBag.idMedicamento = new SelectList(from m in db.Medicamento where m.idMedicamento == idMedicamento select m, "idMedicamento", "nombreMedicamento");

            return View();
        }

        // GET: Lotes/Create
        [CustomAuthorize(Roles = "loteCreate")]
        public ActionResult Create(int? id)
        {
            Medicamento medicamento = new Medicamento();
            var med = from m in db.Medicamento
                      where m.idMedicamento == id
                      select m;

            foreach (Medicamento medi in med)
            {
                medicamento = medi;
                break;
            }

            ViewBag.idMedicamento = new SelectList(from m in db.Medicamento where m.idMedicamento == id select m, "idMedicamento", "nombreMedicamento");

            return View();
        }

        // POST: Lotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "loteCreate")]
        public ActionResult Create([Bind(Include = "idLote,numLote,cantidadLote,fechaVencimiento,idMedicamento")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                db.Lote.Add(lote);
                db.SaveChanges();
                return Json(lote.idLote);
            }

            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento", lote.idMedicamento);
            return Json(lote.idLote);
        }

        // GET: Lotes/Edit/5
        [CustomAuthorize(Roles = "loteEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento", lote.idMedicamento);
            return View(lote);
        }

        // POST: Lotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "loteEdit")]
        public ActionResult Edit([Bind(Include = "idLote,numLote,cantidadLote,fechaVencimiento,idMedicamento")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento", lote.idMedicamento);
            return View(lote);
        }

        // GET: Lotes/Delete/5
        [CustomAuthorize(Roles = "loteoDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            return View(lote);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "loteoDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Lote lote = db.Lote.Find(id);
            db.Lote.Remove(lote);
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
