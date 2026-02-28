using GastosResidenciais.Communication.Enums;

namespace GastosResidenciais.Communication.Responses.Relatorios
{
    public class ResponseTotalCategoriaJson
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoriaEnum Finalidade { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }
}
