﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScanAnalyzer.Modelo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ScanAnalyzeEntities1 : DbContext
    {
        public ScanAnalyzeEntities1()
            : base("name=ScanAnalyzeEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Analis_Rules_Db> Analis_Rules_Db { get; set; }
        public virtual DbSet<Analis_Rules_Port> Analis_Rules_Port { get; set; }
        public virtual DbSet<Analisis> Analisis { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
