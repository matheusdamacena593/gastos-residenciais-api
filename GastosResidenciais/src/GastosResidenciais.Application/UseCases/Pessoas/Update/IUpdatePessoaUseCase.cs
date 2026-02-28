using GastosResidenciais.Communication.Requests;

namespace GastosResidenciais.Application.UseCases.Pessoas.Update
{
    public interface IUpdatePessoaUseCase
    {
        Task Execute(long id, RequestPessoaJson request);
    }
}
