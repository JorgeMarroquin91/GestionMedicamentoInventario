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
    public class VentasController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Ventas
        [CustomAuthorize(Roles = "ventasIndex")]
        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Descuento).Include(v => v.Kardex).Include(v => v.Lote);
            return View(ventas.ToList());
        }

        // GET: Ventas/Details/5
        [CustomAuthorize(Roles = "ventasDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas);
        }

        // GET: Ventas/Create
        [CustomAuthorize(Roles = "ventasCreate")]
        public ActionResult Create(int? idDescuento)
        {

            ViewBag.idDescuento = new SelectList(from d in db.Descuento where d.idDescuento == idDescuento select d, "idDescuento", "idDescuento");
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ventasCreate")]
        public ActionResult Create([Bind(Include = "idVenta,cantidad,precioVenta,idDescuento,idLote,idKardex,fecha")] Ventas ventas)
        {
            var user = SessionPersister.Username;
            var descuento = db.Descuento.FirstOrDefault(d => d.idDescuento == ventas.idDescuento);
            ventas.precioVenta = ventas.precioVenta - descuento.monto;
            var usuario = from u in db.Usuario where u.userName == user select u;
            int cantidad = Convert.ToInt32(ventas.cantidad);
            int userIde = 0;
            int? idInv = 0;

            foreach (Usuario us in usuario)
            {
                userIde = us.idUsuario;
                break;
            }

            var empresas = from a in db.Empresa where a.idUsuario == userIde select a;

            foreach (Empresa empre in empresas)
            {
                idInv = empre.idInventario;
            }

            var inventario = from e in db.Inventario where e.idInventario == idInv select e;

            foreach (Inventario inventari in inventario)
            {
                inventari.existencia = inventari.existencia - cantidad;
                break;
            }

            if (ModelState.IsValid)
            {
                ventas.idLote = db.Ventas.OrderByDescending(v => v.idLote).FirstOrDefault().idLote;
                int sobra = compguar(ventas);
                while (sobra > 0)
                {
                    Ventas venta = new Ventas();
                    venta.idLote = ventas.idLote;
                    venta.cantidad = sobra;
                    venta.Descuento = ventas.Descuento;
                    venta.fecha = ventas.fecha;
                    venta.idKardex = ventas.idKardex;
                    venta.precioVenta = ventas.precioVenta;
                    sobra = compguar(venta);
                }

                Kardex kar = new Kardex();

                return RedirectToAction("Index");
            }

            ViewBag.idDescuento = new SelectList(db.Descuento, "idDescuento", "idDescuento", ventas.idDescuento);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", ventas.idKardex);
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", ventas.idLote);
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        [CustomAuthorize(Roles = "ventasEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDescuento = new SelectList(db.Descuento, "idDescuento", "idDescuento", ventas.idDescuento);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", ventas.idKardex);
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", ventas.idLote);
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ventasEdit")]
        public ActionResult Edit([Bind(Include = "idVenta,cantidad,precioVenta,idDescuento,idLote,idKardex,fecha")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDescuento = new SelectList(db.Descuento, "idDescuento", "idDescuento", ventas.idDescuento);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", ventas.idKardex);
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", ventas.idLote);
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        [CustomAuthorize(Roles = "ventasDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ventasDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ventas ventas = db.Ventas.Find(id);
            db.Ventas.Remove(ventas);
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

        public int compguar(Ventas venta)
        {
            int sobra = 0;
            if (venta.idLote == null)
            {
                var compra = db.Compras.FirstOrDefault();
                sobra = Convert.ToInt32(venta.cantidad - compra.cantidad);
                if (sobra<0)
                {
                    sobra = 0;
                    venta.idLote = compra.idLote;
                    db.Ventas.Add(venta);
                    db.SaveChanges();
                }
                else
                {
                    venta.idLote = compra.idLote;
                    venta.cantidad = compra.cantidad;
                    db.Ventas.Add(venta);
                    db.SaveChanges();
                }

            }//fin if primero
            else
            {
                int idlote = new Int32();
                int totalventa = new Int32();
                var venta2 = db.Ventas.OrderByDescending(v => v.idLote).FirstOrDefault();
                var vent = (from v in db.Ventas where v.idLote == venta2.idLote select v);
                foreach (Ventas venta1 in vent)
                {
                    totalventa = totalventa + Convert.ToInt32(venta1.cantidad);
                    idlote = Convert.ToInt32(venta1.idLote);
                }

                var comp = db.Compras.FirstOrDefault(c => c.idLote == idlote);
                if (totalventa == comp.cantidad)
                {
                    var com = db.Compras.FirstOrDefault(c => c.idLote==idlote + 1);
                    sobra = Convert.ToInt32(venta.cantidad - com.cantidad);
                    if (sobra<=0)
                    {
                        sobra = 0;
                        venta.idLote = com.idLote;
                        db.Ventas.Add(venta);
                        db.SaveChanges();
                    }
                    else
                    {
                        venta.idLote = com.idLote;
                        venta.cantidad = com.cantidad;
                        db.Ventas.Add(venta);
                        db.SaveChanges();
                    }
                }// fin segundo if
                else
                {
                    if (totalventa < comp.cantidad)
                    {
                        int sobrante = Convert.ToInt32(comp.cantidad - totalventa);
                        sobra = Convert.ToInt32(venta.cantidad - sobrante);
                        if (sobra < 0)
                        {
                            sobra = 0;
                            venta.idLote = comp.idLote;
                            db.Ventas.Add(venta);
                            db.SaveChanges();
                        }
                        else
                        {
                        venta.idLote = comp.idLote;
                        venta.cantidad = sobrante;
                        db.Ventas.Add(venta);
                        db.SaveChanges();
                        }
                    }
                }
            }
            return sobra;
        }//fin metodo guardar venta con sobra
    }
}