using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Communication.Responses.Transacoes;

namespace GastosResidenciais.Application.UseCases.Transacoes.GetById
{
    public interface IGetTransacaoByIdUseCase
    {
        Task<ResponseTransacaoJson> Execute(long id);
    }
}
