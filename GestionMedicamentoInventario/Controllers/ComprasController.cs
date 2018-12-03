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
    public class ComprasController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Compras
        [CustomAuthorize(Roles = "comprasIndex")]
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Lote).Include(c => c.Kardex);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        [CustomAuthorize(Roles = "comprasDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // GET: Compras/Create
        [CustomAuthorize(Roles = "comprasCreate")]
        public ActionResult Create(int? idlote)
        {
            if (idlote == null)
            {
                ViewBag.idKardex = new SelectList(db.Medicamento,"idMedicamento","nombreMedicamento");
                //ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex");
                ViewBag.idLote = new SelectList(from l in db.Lote where l.idLote == null select l, "idLote", "idLote");
                return View();
            }

            var idkardex = db.Lote.FirstOrDefault(i => i.idLote == idlote).idMedicamento;
                
            ViewBag.idKardex = new SelectList(from m in db.Medicamento where m.idMedicamento == idkardex select m, "idMedicamento", "nombreMedicamento");
            ViewBag.idLote = new SelectList(from l in db.Lote where l.idLote == idlote select l, "idLote", "idLote");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "comprasCreate")]
        public ActionResult Create([Bind(Include = "idCompra,cantidad,precioCompra,idLote,idKardex,fecha")] Compras compras)
        {
            var user = SessionPersister.Username;
            var usuario= from u in db.Usuario where u.userName == user select u;
            int existencia = Convert.ToInt32(compras.cantidad);
            int userIde=0;
            int? idInv = 0;

            foreach (Usuario us in usuario)
            {
                userIde = us.idUsuario;
                break;
            }

            var empresas = from i in db.Empresa where i.idUsuario == userIde select i;

            foreach (Empresa empre in empresas)
            {
                idInv = empre.idInventario;
            }

            var inventario = from i in db.Inventario where i.idInventario == idInv select i;

            foreach (Inventario inventari in inventario)
            {
                inventari.existencia = inventari.existencia + existencia;
                break;
            }

            var kar = from k in db.Kardex where k.idMedicamento == compras.idKardex select k;

            foreach (Kardex kard in kar)
            {
                kard.saldo = kard.saldo + compras.cantidad;
                break;
            }

            Detalle det = new Detalle();

            det.idInventario = idInv;
            det.idLote = compras.idLote;

            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.Detalle.Add(det);
                db.SaveChanges();
                return Json(compras.idCompra);
            }

            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", compras.idLote);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", compras.idKardex);
            return View(compras);
        }

        // GET: Compras/Edit/5
        [CustomAuthorize(Roles = "comprasEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", compras.idLote);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", compras.idKardex);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "comprasEdit")]
        public ActionResult Edit([Bind(Include = "idCompra,cantidad,precioCompra,idLote,idKardex,fecha")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", compras.idLote);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", compras.idKardex);
            return View(compras);
        }

        // GET: Compras/Delete/5
        [CustomAuthorize(Roles = "comprasDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "comprasDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
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
