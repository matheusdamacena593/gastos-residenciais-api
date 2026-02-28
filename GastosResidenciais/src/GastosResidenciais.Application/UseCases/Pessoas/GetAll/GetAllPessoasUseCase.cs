using AutoMapper;
using GastosResidenciais.Communication.Responses.Pessoas;
using GastosResidenciais.Domain.Repositories.Pessoas;

namespace GastosResidenciais.Application.UseCases.Pessoas.GetAll
{
    public class GetAllPessoasUseCase : IGetAllPessoasUseCase
    {
        private readonly IPessoaReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPessoasUseCase(
            IPessoaReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponsePessoasJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponsePessoasJson
            {
                Pessoas = _mapper.Map<List<ResponsePessoaJson>>(result)
            };
        }
    }
}
