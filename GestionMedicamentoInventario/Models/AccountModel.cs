using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionMedicamentoInventario.Models;

namespace GestionMedicamentoInventario.Models
{
    public class AccountModel
    {
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();
        private List<Usuario> listAccount = new List<Usuario>();

        public Usuario find(string username)
        {
            return db.Usuario.Where(acc => acc.userName.Equals(username)).FirstOrDefault();
        }

        public Usuario login(string username, string password)
        {
            Usuario user = new Usuario();
            var usuario = from u in db.Usuario
                          where u.userName == username && u.password == password
                          select u;

            foreach (Usuario us in usuario)
            {
                user = us;
                break;
            }

            return user;
        }

    }
}