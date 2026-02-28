using GastosResidenciais.Communication.Responses.Pessoas;

namespace GastosResidenciais.Application.UseCases.Pessoas.GetAll
{
    public interface IGetAllPessoasUseCase
    {
        Task<ResponsePessoasJson> Execute();
    }
}
