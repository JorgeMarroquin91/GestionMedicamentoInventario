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
    public class DescuentoesController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Descuentoes
        [CustomAuthorize(Roles = "descuentoIndex")]
        public ActionResult Index()
        {
            return View(db.Descuento.ToList());
        }

        // GET: Descuentoes/Details/5
        [CustomAuthorize(Roles = "descuentoDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descuento descuento = db.Descuento.Find(id);
            if (descuento == null)
            {
                return HttpNotFound();
            }
            return View(descuento);
        }

        // GET: Descuentoes/Create
        [CustomAuthorize(Roles = "descuentoCreate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Descuentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "descuentoCreate")]
        public ActionResult Create([Bind(Include = "idDescuento,monto,porcentaje")] Descuento descuento)
        {
            if (ModelState.IsValid)
            {
                db.Descuento.Add(descuento);
                db.SaveChanges();
                return Json(descuento.idDescuento);
            }

            return View(descuento);
        }

        // GET: Descuentoes/Edit/5
        [CustomAuthorize(Roles = "descuentoEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descuento descuento = db.Descuento.Find(id);
            if (descuento == null)
            {
                return HttpNotFound();
            }
            return View(descuento);
        }

        // POST: Descuentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "descuentoEdit")]
        public ActionResult Edit([Bind(Include = "idDescuento,monto,porcentaje")] Descuento descuento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descuento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descuento);
        }

        // GET: Descuentoes/Delete/5
        [CustomAuthorize(Roles = "descuentoDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descuento descuento = db.Descuento.Find(id);
            if (descuento == null)
            {
                return HttpNotFound();
            }
            return View(descuento);
        }

        // POST: Descuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "descuentoDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Descuento descuento = db.Descuento.Find(id);
            db.Descuento.Remove(descuento);
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
