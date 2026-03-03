using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories.Transacoes;

namespace GastosResidenciais.Application.UseCases.Transacoes.Count
{
    public class GetTransacoesCountUseCase : IGetTransacoesCountUseCase
    {
        private readonly ITransacaoReadOnlyRepository _repository;

        public GetTransacoesCountUseCase(ITransacaoReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseCountJson> Execute()
        {
            var total = await _repository.CountAsync();

            return new ResponseCountJson { Total = total };
        }
    }
}
