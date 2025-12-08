using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TechStore.Entidades;

namespace TechStore.Datos
{
    public class TechStoreContext : DbContext
    {
        // Constructor con cadena de conexión
        public TechStoreContext() : base("name=TechStoreConnection")
        {
            // Deshabilitar la inicialización de base de datos
            Database.SetInitializer<TechStoreContext>(null);
        }

        // DbSets - Representan las tablas
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<TipoCliente> TiposCliente { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<CuentaCorriente> CuentasCorrientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuraciones adicionales de precisión decimal
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.LimiteCredito)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.SaldoActual)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
                .Property(v => v.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
                .Property(v => v.MontoDescuento)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.Subtotal)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
