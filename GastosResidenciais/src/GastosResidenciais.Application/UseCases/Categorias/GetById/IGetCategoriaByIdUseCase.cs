using GastosResidenciais.Communication.Responses.Categorias;

namespace GastosResidenciais.Application.UseCases.Categorias.GetById
{
    public interface IGetCategoriaByIdUseCase
    {
        Task<ResponseCategoriaJson> Execute(long id);
    }
}
