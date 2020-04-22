using System;
using Microsoft.EntityFrameworkCore;
using VentasApp.Models;

namespace VentasApp.Models
{
    public class ContextBase : DbContext
    {
        public ContextBase (DbContextOptions<ContextBase> options)
            : base(options)
        {
        }

        public DbSet<Tiendas> Tiendas {get; set;}
        public DbSet<Productos> Productos {get; set;}
        public DbSet<Categorias> Categorias {get; set;}
        public DbSet<Ventas> Ventas {get; set;}
        public DbSet<DetalleVentas> DetalleVentas {get; set;}
    }
}