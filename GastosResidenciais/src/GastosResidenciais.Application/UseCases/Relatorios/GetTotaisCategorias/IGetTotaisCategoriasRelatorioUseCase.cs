using GastosResidenciais.Communication.Responses.Relatorios;

namespace GastosResidenciais.Application.UseCases.Relatorios.GetTotaisCategorias
{
    public interface IGetTotaisCategoriasRelatorioUseCase
    {
        Task<ResponseTotaisCategoriasJson> Execute();
    }
}
