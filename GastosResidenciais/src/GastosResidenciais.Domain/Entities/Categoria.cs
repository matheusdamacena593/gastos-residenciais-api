using GastosResidenciais.Domain.Entities.BaseEntities;
using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Domain.Entities
{
    public class Categoria : IBaseEntity
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoriaEnum Finalidade { get; set; }
        public ICollection<Transacao> Transacoes { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
