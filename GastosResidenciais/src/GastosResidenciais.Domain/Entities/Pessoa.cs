using GastosResidenciais.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Domain.Entities
{
    public class Pessoa : IBaseEntity
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public ICollection<Transacao> Transacoes { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
