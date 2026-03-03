using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.DTOs;

namespace GastosResidenciais.Application.UseCases.Transacoes.GetAll
{
    public interface IGetAllTransacoesUseCase
    {
        Task<PageResultDTO<ResponseTransacaoJson>> Execute(int page, int pageSize);
    }
}
