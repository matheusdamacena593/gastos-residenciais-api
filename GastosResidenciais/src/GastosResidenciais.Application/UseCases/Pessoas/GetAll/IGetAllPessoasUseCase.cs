using GastosResidenciais.Communication.Responses.Pessoas;
using GastosResidenciais.Domain.DTOs;

namespace GastosResidenciais.Application.UseCases.Pessoas.GetAll
{
    public interface IGetAllPessoasUseCase
    {
        Task<PageResultDTO<ResponsePessoaJson>> Execute(int page, int pageSize);
    }
}
