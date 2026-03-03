using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories.Pessoas;

namespace GastosResidenciais.Application.UseCases.Pessoas.Count
{
    public class GetPessoasCountUseCase : IGetPessoasCountUseCase
    {
        private readonly IPessoaReadOnlyRepository _repository;

        public GetPessoasCountUseCase(IPessoaReadOnlyRepository repository)
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
