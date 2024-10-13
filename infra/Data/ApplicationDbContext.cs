using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lanchonete.infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                }
            }

            base.OnModelCreating(modelBuilder);

            //PedidoItem
            modelBuilder.Entity<PedidoItem>()
                .HasKey(pi => pi.Id);

            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(pi => pi.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Produto)
                .WithMany()
                .HasForeignKey(pi => pi.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PedidoItem>(entity =>
            {
                entity.Property(pi => pi.Quantidade)
                    .IsRequired();

                entity.Property(pi => pi.Customizacao)
                    .HasMaxLength(200);

                entity.Property(pi => pi.Valor)
                    .IsRequired();

                entity.Property(pi => pi.DataCriacao)
                    .IsRequired();
            });


            // Data Seeding para a tabela Products
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Lanche", Descricao = "", Ordem = 1 },
                new Categoria { Id = 2, Nome = "Acompanhamento", Descricao = "", Ordem = 2 },
                new Categoria { Id = 3, Nome = "Bebida", Descricao = "", Ordem = 3 },
                new Categoria { Id = 4, Nome = "Sobremesa", Descricao = "", Ordem = 4 }
            );
        }
    }
}
