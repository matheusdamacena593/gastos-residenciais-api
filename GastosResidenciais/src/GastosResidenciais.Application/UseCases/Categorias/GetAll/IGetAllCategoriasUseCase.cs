using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Domain.DTOs;

namespace GastosResidenciais.Application.UseCases.Categorias.GetAll
{
    public interface IGetAllCategoriasUseCase
    {
        Task<PageResultDTO<ResponseCategoriaJson>> Execute(int page, int pageSize);
    }
}
