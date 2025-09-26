using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(s => s.Id)
                .ValueGeneratedNever()
                .IsRequired(); // Specify that AgenteId is not auto-generated and is required

            modelBuilder.Entity<User>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ValorPedido)
                .HasPrecision(18, 2); // Define the precision and scale for decimal properties
            base.OnModelCreating(modelBuilder);
        }

        // Define your DbSet properties for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Relatorio> Relatorios { get; set; } = null!;
        public DbSet<UserLogins> UserLogins { get; set; } = null!;
    }
}
