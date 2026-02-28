using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Transacoes;

namespace GastosResidenciais.Application.UseCases.Transacoes.Register
{
    public interface IRegisterTransacaoUseCase
    {
        Task<ResponseTransacaoJson> Execute(RequestTransacaoJson request);
    }
}
