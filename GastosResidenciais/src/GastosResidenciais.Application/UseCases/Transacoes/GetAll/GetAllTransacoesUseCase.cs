using AutoMapper;
using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.Repositories.Transacoes;

namespace GastosResidenciais.Application.UseCases.Transacoes.GetAll
{
    public class GetAllTransacoesUseCase : IGetAllTransacoesUseCase
    {
        private readonly ITrancasaoReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTransacoesUseCase(
            ITrancasaoReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseTransacoesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseTransacoesJson
            {
                Transacoes = _mapper.Map<List<ResponseTransacaoJson>>(result)
            };
        }
    }
}
