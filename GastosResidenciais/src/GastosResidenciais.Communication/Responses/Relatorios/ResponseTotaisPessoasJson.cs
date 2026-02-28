namespace GastosResidenciais.Communication.Responses.Relatorios
{
    public class ResponseTotaisPessoasJson
    {
        public List<ResponseTotalPessoaJson> Pessoas { get; set; } = [];
        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }
        public decimal SaldoGeral { get; set; }
    }
}
