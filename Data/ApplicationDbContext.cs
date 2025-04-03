using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proyectoTienda.Models;
namespace proyectoTienda.Data;

public class ApplicationDbContext : IdentityDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }
  public DbSet<Usuario> Usuarios { get; set; }
  public DbSet<Producto> Productos { get; set; }
  public DbSet<Pedido> Pedidos { get; set; }
  public DbSet<DetallePedido> DetallesPedidos { get; set; }
  public DbSet<Categoria> Categorias { get; set; }
  public DbSet<Pago> Pagos { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar clave primaria compuesta para DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasKey(d => new { d.IDPedido, d.IDProducto });

            // Restricciones
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique();

            modelBuilder.Entity<Pedido>()
                .Property(p => p.FechaPedido)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Pago>()
                .Property(p => p.FechaPago)
                .HasDefaultValueSql("GETDATE()");

            

            

            

            modelBuilder.Entity<Pago>()
                .HasIndex(p => p.IDPedido)
                .IsUnique();
        }
}
