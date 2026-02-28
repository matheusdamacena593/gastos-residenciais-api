using GastosResidenciais.Communication.Enums;

namespace GastosResidenciais.Communication.Requests
{
    public class RequestCategoriaJson
    {
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoriaEnum Finalidade { get; set; }
    }
}
