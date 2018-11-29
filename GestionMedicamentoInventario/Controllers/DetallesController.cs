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
    public class DetallesController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Detalles
        [CustomAuthorize(Roles = "detalleIndex")]
        public ActionResult Index()
        {
            var detalle = db.Detalle.Include(d => d.Lote).Include(d => d.Inventario);
            return View(detalle.ToList());
        }

        // GET: Detalles/Details/5
        [CustomAuthorize(Roles = "detalleDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // GET: Detalles/Create
        [CustomAuthorize(Roles = "detalleCreate")]
        public ActionResult Create()
        {
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote");
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario");
            return View();
        }

        // POST: Detalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "detalleCreate")]
        public ActionResult Create([Bind(Include = "idDetalle,idLote,idInventario")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Detalle.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", detalle.idLote);
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario", detalle.idInventario);
            return View(detalle);
        }

        // GET: Detalles/Edit/5
        [CustomAuthorize(Roles = "detalleEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", detalle.idLote);
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario", detalle.idInventario);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "detalleEdit")]
        public ActionResult Edit([Bind(Include = "idDetalle,idLote,idInventario")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", detalle.idLote);
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario", detalle.idInventario);
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        [CustomAuthorize(Roles = "detalleDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "detalleDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            db.Detalle.Remove(detalle);
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
