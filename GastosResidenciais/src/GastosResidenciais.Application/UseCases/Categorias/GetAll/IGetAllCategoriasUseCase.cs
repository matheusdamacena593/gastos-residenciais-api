using GastosResidenciais.Communication.Responses.Categorias;

namespace GastosResidenciais.Application.UseCases.Categorias.GetAll
{
    public interface IGetAllCategoriasUseCase
    {
        Task<ResponseCategoriasJson> Execute();
    }
}
