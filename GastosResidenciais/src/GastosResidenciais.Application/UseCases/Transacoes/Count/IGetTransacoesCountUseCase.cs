using GastosResidenciais.Communication.Responses;

namespace GastosResidenciais.Application.UseCases.Transacoes.Count
{
    public interface IGetTransacoesCountUseCase
    {
        Task<ResponseCountJson> Execute();
    }
}
