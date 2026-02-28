using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Domain.DTOs
{
    public class CategoriaTotaisDTO
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoriaEnum Finalidade { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
    }
}
