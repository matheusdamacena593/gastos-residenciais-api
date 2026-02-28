using GastosResidenciais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastosResidenciais.Infrastructure.DataAccess.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao).HasMaxLength(400).IsRequired();
            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(x => x.TipoTransacao).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();
            builder.Property(x => x.PessoaId).IsRequired();

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

            builder.HasOne(c => c.Categoria)
                .WithMany(p => p.Transacoes)
                .HasForeignKey(c => c.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Pessoa)
                .WithMany(p => p.Transacoes)
                .HasForeignKey(c => c.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PessoaId);

            builder.HasIndex(x => x.CategoriaId);
        }
    }
}
