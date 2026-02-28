using GastosResidenciais.Communication.Enums;

namespace GastosResidenciais.Communication.Requests
{
    public class RequestTransacaoJson
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public long CategoriaId { get; set; }
        public long PessoaId { get; set; }
    }
}
