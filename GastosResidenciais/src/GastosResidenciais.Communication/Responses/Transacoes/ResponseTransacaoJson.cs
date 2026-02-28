using GastosResidenciais.Communication.Enums;

namespace GastosResidenciais.Communication.Responses.Transacoes
{
    public class ResponseTransacaoJson
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public long CategoriaId { get; set; }
        public long PessoaId { get; set; }
    }
}
