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
    public class MedicamentoesController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Medicamentoes
        [CustomAuthorize(Roles = "medicamentoIndex")]
        public ActionResult Index()
        {
            var medicamento = db.Medicamento.Include(m => m.Categoria);
            return View(medicamento.ToList());
        }

        // GET: Medicamentoes/Details/5
        [CustomAuthorize(Roles = "medicamentoDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = db.Medicamento.Find(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            return View(medicamento);
        }

        // GET: Medicamentoes/Create
        [CustomAuthorize(Roles = "medicamentoCreate")]
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombreCategoria");
            return View();
        }

        // POST: Medicamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "medicamentoCreate")]
        public ActionResult Create([Bind(Include = "idMedicamento,nombreMedicamento,descripcionMedicamento,idCategoria")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                db.Medicamento.Add(medicamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombreCategoria", medicamento.idCategoria);
            return View(medicamento);
        }

        // GET: Medicamentoes/Edit/5
        [CustomAuthorize(Roles = "medicamentoEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = db.Medicamento.Find(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombreCategoria", medicamento.idCategoria);
            return View(medicamento);
        }

        // POST: Medicamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "medicamentoEdit")]
        public ActionResult Edit([Bind(Include = "idMedicamento,nombreMedicamento,descripcionMedicamento,idCategoria")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombreCategoria", medicamento.idCategoria);
            return View(medicamento);
        }

        // GET: Medicamentoes/Delete/5
        [CustomAuthorize(Roles = "medicamentoDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = db.Medicamento.Find(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            return View(medicamento);
        }

        // POST: Medicamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "medicamentoDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicamento medicamento = db.Medicamento.Find(id);
            db.Medicamento.Remove(medicamento);
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
