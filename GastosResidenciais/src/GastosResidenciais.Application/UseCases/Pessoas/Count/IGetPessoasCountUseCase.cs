using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Pessoas.Count
{
    public interface IGetPessoasCountUseCase
    {
        Task<ResponseCountJson> Execute();
    }
}
