using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Categorias;

namespace GastosResidenciais.Application.UseCases.Categorias.Register
{
    public interface IRegisterCategoriaUseCase
    {
        Task<ResponseCategoriaJson> Execute(RequestCategoriaJson request);
    }
}
