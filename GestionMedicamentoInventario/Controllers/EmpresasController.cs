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
    public class EmpresasController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        // GET: Empresas
        [CustomAuthorize(Roles = "empresaIndex")]
        public ActionResult Index()
        {
            var empresa = db.Empresa.Include(e => e.Inventario).Include(e => e.Usuario);
            return View(empresa.ToList());
        }

        // GET: Empresas/Details/5
        [CustomAuthorize(Roles = "empresaDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        [CustomAuthorize(Roles = "empresaCreate")]
        public ActionResult Create()
        {
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario");
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "empresaCreate")]
        public ActionResult Create([Bind(Include = "idEmpresa,nombreEmpresa,direcicon,telefonoEmpresa,nacionalidad,idUsuario,idInventario")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Empresa.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario", empresa.idInventario);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario", empresa.idUsuario);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        [CustomAuthorize(Roles = "empresaEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario", empresa.idInventario);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario", empresa.idUsuario);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "empresaEdit")]
        public ActionResult Edit([Bind(Include = "idEmpresa,nombreEmpresa,direcicon,telefonoEmpresa,nacionalidad,idUsuario,idInventario")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idInventario = new SelectList(db.Inventario, "idInventario", "idInventario", empresa.idInventario);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "nombreUsuario", empresa.idUsuario);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        [CustomAuthorize(Roles = "empresaDelete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "empresaDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresa.Find(id);
            db.Empresa.Remove(empresa);
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
