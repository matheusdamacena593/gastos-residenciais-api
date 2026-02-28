namespace GastosResidenciais.Communication.Responses.Relatorios
{
    public class ResponseTotalPessoaJson
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }
}
