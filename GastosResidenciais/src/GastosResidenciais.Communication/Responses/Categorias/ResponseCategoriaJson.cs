using GastosResidenciais.Communication.Enums;

namespace GastosResidenciais.Communication.Responses.Categorias
{
    public class ResponseCategoriaJson
    {
        public long Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoriaEnum Finalidade { get; set; }
    }
}
