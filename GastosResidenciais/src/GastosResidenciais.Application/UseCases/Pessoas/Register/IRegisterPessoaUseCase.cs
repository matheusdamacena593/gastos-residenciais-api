using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Pessoas;

namespace GastosResidenciais.Application.UseCases.Pessoas.Register
{
    public interface IRegisterPessoaUseCase
    {
        Task<ResponsePessoaJson> Execute(RequestPessoaJson request);
    }
}
