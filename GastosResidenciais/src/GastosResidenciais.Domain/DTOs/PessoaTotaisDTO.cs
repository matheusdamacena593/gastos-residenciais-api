namespace GastosResidenciais.Domain.DTOs
{
    public class PessoaTotaisDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
    }
}
