using GastosResidenciais.Communication.Responses.Transacoes;

namespace GastosResidenciais.Application.UseCases.Transacoes.GetAll
{
    public interface IGetAllTransacoesUseCase
    {
        Task<ResponseTransacoesJson> Execute();
    }
}
