using GastosResidenciais.Communication.DTOs;

namespace GastosResidenciais.Communication.Responses.Relatorios
{
    public class ResponseTotaisPessoasJson
    {
        public PageResultDTO<ResponseTotalPessoaJson> Pessoas { get; set; }
        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }
        public decimal SaldoGeral { get; set; }
    }
}
