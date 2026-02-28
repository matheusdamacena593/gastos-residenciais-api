using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess
{
    internal class GastosResidenciaisDbContext : DbContext
    {
        public GastosResidenciaisDbContext(DbContextOptions<GastosResidenciaisDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new TransacaoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
