using GastosResidenciais.Domain.Entities.BaseEntities;
using GastosResidenciais.Domain.Enums;
using System.Text.Json.Serialization;

namespace GastosResidenciais.Domain.Entities
{
    public class Transacao : IBaseEntity
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public long CategoriaId { get; set; }
        public long PessoaId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Pessoa Pessoa { get; set; }
        public Categoria Categoria { get; set; }
    }
}
