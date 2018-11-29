using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using GestionMedicamentoInventario.Models;

namespace GestionMedicamentoInventario.Seguridad
{
    public class CustomPrincipal : IPrincipal
    {
        private Usuario Account;
        private DB_A41D6A_GestionInventarioEntities1 db = new DB_A41D6A_GestionInventarioEntities1();

        public CustomPrincipal(Usuario account)
        {
            this.Account = account;
            this.Identity = new GenericIdentity(account.userName);
        }

        public IIdentity Identity
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            int? i = 0;
            string user = this.Account.userName;
            var userid = from ui in db.Usuario where ui.userName == user select ui;

            foreach (Usuario u in userid)
            {
                i = u.idRol;
                break;
            }

            var ro = from u in db.Rol where u.idRol==i select u;

            foreach (Rol r in ro)
            {
                //Roles de Categoria
                if (role == "categoriaCreate")
                {
                    if (r.categoriaCreate)
                    {
                        return true;
                    }
                }

                if (role == "categoriaDelete")
                {
                    if (r.categoriaDelete)
                    {
                        return true;
                    }
                }

                if (role == "categoriaDetails")
                {
                    if (r.categoriaDetails)
                    {
                        return true;
                    }
                }

                if (role == "categoriaEdit")
                {
                    if (r.categoriaEdit)
                    {
                        return true;
                    }
                }

                if (role == "categoriaIndex")
                {
                    if (r.categoriaIndex)
                    {
                        return true;
                    }
                }




                //Roles de Descuento
                if (role == "descuentoCreate")
                {
                    if (r.descuentoCreate)
                    {
                        return true;
                    }
                }

                if (role == "descuentoDelete")
                {
                    if (r.descuentoDelete)
                    {
                        return true;
                    }
                }

                if (role == "descuentoDetails")
                {
                    if (r.descuentoDetails)
                    {
                        return true;
                    }
                }

                if (role == "descuentoEdit")
                {
                    if (r.descuentoEdit)
                    {
                        return true;
                    }
                }

                if (role == "descuentoIndex")
                {
                    if (r.descuentoIndex)
                    {
                        return true;
                    }
                }




                //Roles de Detalle
                if (role == "detallesCreate")
                {
                    if (r.detallesCreate)
                    {
                        return true;
                    }
                }

                if (role == "detallesDelete")
                {
                    if (r.detallesDelete)
                    {
                        return true;
                    }
                }

                if (role == "detallesDetails")
                {
                    if (r.detallesDetails)
                    {
                        return true;
                    }
                }

                if (role == "detallesEdit")
                {
                    if (r.detallesEdit)
                    {
                        return true;
                    }
                }

                if (role == "detallesIndex")
                {
                    if (r.detallesIndex)
                    {
                        return true;
                    }
                }




                //Roles de Empresa
                if (role == "empresaCreate")
                {
                    if (r.empresaCreate)
                    {
                        return true;
                    }
                }

                if (role == "empresaDelete")
                {
                    if (r.empresaDelete)
                    {
                        return true;
                    }
                }

                if (role == "empresaDetails")
                {
                    if (r.empresaDetails)
                    {
                        return true;
                    }
                }

                if (role == "empresaEdit")
                {
                    if (r.empresaEdit)
                    {
                        return true;
                    }
                }

                if (role == "empresaIndex")
                {
                    if (r.empresaIndex)
                    {
                        return true;
                    }

                }




                //Roles de Inventario
                if (role == "inventarioCreate")
                {
                    if (r.inventarioCreate)
                    {
                        return true;
                    }
                }

                if (role == "inventarioDelete")
                {
                    if (r.inventarioDelete)
                    {
                        return true;
                    }
                }

                if (role == "inventarioDetails")
                {
                    if (r.inventarioDetails)
                    {
                        return true;
                    }
                }

                if (role == "inventarioEdit")
                {
                    if (r.inventarioEdit)
                    {
                        return true;
                    }
                }

                if (role == "inventarioIndex")
                {
                    if (r.inventarioIndex)
                    {
                        return true;
                    }
                }




                //Roles de Kardex
                if (role== "kardexCreate")
                {
                    if (r.kardexCreate)
                    {
                        return true;
                    }
                }

                if (role == "kardexDelete")
                {
                    if (r.kardexDelete)
                    {
                        return true;
                    }
                }

                if (role == "kardexDetails")
                {
                    if (r.kardexDetails)
                    {
                        return true;
                    }
                }

                if (role == "kardexEdit")
                {
                    if (r.kardexEdit)
                    {
                        return true;
                    }
                }

                if (role == "kardexIndex")
                {
                    if (r.kardexIndex)
                    {
                        return true;
                    }
                }




                //Roles de Lote
                if (role== "loteCreate")
                {
                    if (r.loteCreate)
                    {
                        return true;
                    }
                }

                if (role == "loteoDelete")
                {
                    if (r.loteoDelete)
                    {
                        return true;
                    }
                }

                if (role == "loteDetails")
                {
                    if (r.loteDetails)
                    {
                        return true;
                    }
                }

                if (role == "loteEdit")
                {
                    if (r.loteEdit)
                    {
                        return true;
                    }
                }

                if (role == "loteIndex")
                {
                    if (r.loteIndex)
                    {
                        return true;
                    }
                }




                //roles de medicamento
                if (role== "medicamentoCreate")
                {
                    if (r.medicamentoCreate)
                    {
                        return true;
                    }
                }

                if (role == "medicamentoDelete")
                {
                    if (r.medicamentoDelete)
                    {
                        return true;
                    }
                }

                if (role == "medicamentoDetails")
                {
                    if (r.medicamentoDetails)
                    {
                        return true;
                    }
                }

                if (role == "medicamentoEdit")
                {
                    if (r.medicamentoEdit)
                    {
                        return true;
                    }
                }

                if (role == "medicamentoIndex")
                {
                    if (r.medicamentoIndex)
                    {
                        return true;
                    }
                }




                //roles de rol
                if (role== "rolCreate")
                {
                    if (r.rolCreate)
                    {
                        return true;
                    }
                }

                if (role == "rolDelete")
                {
                    if (r.rolDelete)
                    {
                        return true;
                    }
                }

                if (role == "rolDetails")
                {
                    if (r.rolDetails)
                    {
                        return true;
                    }
                }

                if (role == "rolEdit")
                {
                    if (r.rolEdit)
                    {
                        return true;
                    }
                }

                if (role == "rolIndex")
                {
                    if (r.rolIndex)
                    {
                        return true;
                    }
                }



                //roles de usuario
                if (role== "usuarioCreate")
                {
                    if (r.usuarioCreate)
                    {
                        return true;
                    }
                }
                if (role == "usuarioDelete")
                {
                    if (r.usuarioDelete)
                    {
                        return true;
                    }
                }
                if (role == "usuarioDetails")
                {
                    if (r.usuarioDetails)
                    {
                        return true;
                    }
                }
                if (role== "usuarioEdit")
                {
                    if (r.usuarioEdit)
                    {
                        return true;
                    }
                }
                if (role == "usuarioIndex")
                {
                    if (r.usuarioIndex)
                    {
                        return true;
                    }
                }



                //roles de ventas 
                if (role == "ventasCreate")
                {
                    if (r.ventasCreate)
                    {
                        return true;
                    }
                }

                if (role == "ventasDelete")
                {
                    if (r.ventasDelete)
                    {
                        return true;
                    }
                }

                if (role == "ventasDetails")
                {
                    if (r.ventasDetails)
                    {
                        return true;
                    }
                }

                if (role == "ventasEdit")
                {
                    if (r.ventasEdit)
                    {
                        return true;
                    }
                }

                if (role == "ventasIndex")
                {
                    if (r.ventasIndex)
                    {
                        return true;
                    }
                }

                //roles de ventas 
                if (role == "comprasCreate")
                {
                    if (r.comprasCreate)
                    {
                        return true;
                    }
                }

                if (role == "comprasDelete")
                {
                    if (r.comprasDelete)
                    {
                        return true;
                    }
                }

                if (role == "comprasDetails")
                {
                    if (r.comprasDetails)
                    {
                        return true;
                    }
                }

                if (role == "comprasEdit")
                {
                    if (r.comprasEdit)
                    {
                        return true;
                    }
                }

                if (role == "comprasIndex")
                {
                    if (r.comprasIndex)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
    }
}