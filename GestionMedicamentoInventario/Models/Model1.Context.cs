﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionMedicamentoInventario.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A41D6A_GestionInventarioEntities1 : DbContext
    {
        public DB_A41D6A_GestionInventarioEntities1()
            : base("name=DB_A41D6A_GestionInventarioEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Compras> Compras { get; set; }
        public virtual DbSet<Descuento> Descuento { get; set; }
        public virtual DbSet<Detalle> Detalle { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Kardex> Kardex { get; set; }
        public virtual DbSet<Lote> Lote { get; set; }
        public virtual DbSet<Medicamento> Medicamento { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }
    }
}