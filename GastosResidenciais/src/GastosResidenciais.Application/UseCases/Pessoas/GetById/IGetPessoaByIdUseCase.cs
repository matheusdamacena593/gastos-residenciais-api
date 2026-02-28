using GastosResidenciais.Communication.Responses.Pessoas;

namespace GastosResidenciais.Application.UseCases.Pessoas.GetById
{
    public interface IGetPessoaByIdUseCase
    {
        Task<ResponsePessoaJson> Execute(long id);
    }
}
