using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Categorias.Count
{
    public interface IGetCategoriasCountUseCase
    {
        Task<ResponseCountJson> Execute();
    }
}
