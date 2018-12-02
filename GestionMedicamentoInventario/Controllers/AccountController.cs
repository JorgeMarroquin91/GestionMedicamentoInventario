using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionMedicamentoInventario.Models;
using GestionMedicamentoInventario.Seguridad;

namespace GestionMedicamentoInventario.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            AccountModel am = new AccountModel();
            if (string.IsNullOrEmpty(avm.Account.userName) || string.IsNullOrEmpty(avm.Account.password) || am.login(avm.Account.userName, avm.Account.password).userName == null || am.login(avm.Account.userName, avm.Account.password).password == null)
            {
                ViewBag.Error = "Usuario No Valido";
                return View("Index");
            }
            SessionPersister.Username = avm.Account.userName;
            return RedirectToAction("Index","Usuarios");
        }

        public ActionResult Logout()
        {
            SessionPersister.Username = string.Empty;
            return RedirectToAction("Index");
        }
    }
}