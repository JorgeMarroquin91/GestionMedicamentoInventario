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
    public class RolsController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Rols
        [CustomAuthorize(Roles = "rolIndex")]
        public ActionResult Index()
        {
            return View(db.Rol.ToList());
        }

        // GET: Rols/Details/5
        [CustomAuthorize(Roles = "rolDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // GET: Rols/Create
        [CustomAuthorize(Roles = "rolCreate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "rolCreate")]
        public ActionResult Create([Bind(Include = "idRol,categoriaCreate,categoriaDelete,categoriaDetails,categoriaEdit,categoriaIndex,comprasCreate,comprasDelete,comprasDetails,comprasEdit,comprasIndex,descuentoCreate,descuentoDelete,descuentoDetails,descuentoEdit,descuentoIndex,detallesCreate,detallesDelete,detallesDetails,detallesEdit,detallesIndex,empresaCreate,empresaDelete,empresaDetails,empresaEdit,empresaIndex,inventarioCreate,inventarioDelete,inventarioDetails,inventarioEdit,inventarioIndex,kardexCreate,kardexDelete,kardexDetails,kardexEdit,kardexIndex,loteCreate,loteoDelete,loteDetails,loteEdit,loteIndex,medicamentoCreate,medicamentoDelete,medicamentoDetails,medicamentoEdit,medicamentoIndex,rolCreate,rolDelete,rolDetails,rolEdit,rolIndex,usuarioCreate,usuarioDelete,usuarioDetails,usuarioEdit,usuarioIndex,ventasCreate,ventasDelete,ventasDetails,ventasEdit,ventasIndex")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Rol.Add(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        // GET: Rols/Edit/5
        [CustomAuthorize(Roles = "rolEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Rols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "rolEdit")]
        public ActionResult Edit([Bind(Include = "idRol,categoriaCreate,categoriaDelete,categoriaDetails,categoriaEdit,categoriaIndex,comprasCreate,comprasDelete,comprasDetails,comprasEdit,comprasIndex,descuentoCreate,descuentoDelete,descuentoDetails,descuentoEdit,descuentoIndex,detallesCreate,detallesDelete,detallesDetails,detallesEdit,detallesIndex,empresaCreate,empresaDelete,empresaDetails,empresaEdit,empresaIndex,inventarioCreate,inventarioDelete,inventarioDetails,inventarioEdit,inventarioIndex,kardexCreate,kardexDelete,kardexDetails,kardexEdit,kardexIndex,loteCreate,loteoDelete,loteDetails,loteEdit,loteIndex,medicamentoCreate,medicamentoDelete,medicamentoDetails,medicamentoEdit,medicamentoIndex,rolCreate,rolDelete,rolDetails,rolEdit,rolIndex,usuarioCreate,usuarioDelete,usuarioDetails,usuarioEdit,usuarioIndex,ventasCreate,ventasDelete,ventasDetails,ventasEdit,ventasIndex")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        // GET: Rols/Delete/5
        [CustomAuthorize(Roles = "rolDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "rolDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rol rol = db.Rol.Find(id);
            db.Rol.Remove(rol);
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
