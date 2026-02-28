namespace GastosResidenciais.Communication.Responses.Relatorios
{
    public class ResponseTotaisCategoriasJson
    {
        public List<ResponseTotalCategoriaJson> Categorias { get; set; } = [];
        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }
        public decimal SaldoGeral { get; set; }
    }
}
