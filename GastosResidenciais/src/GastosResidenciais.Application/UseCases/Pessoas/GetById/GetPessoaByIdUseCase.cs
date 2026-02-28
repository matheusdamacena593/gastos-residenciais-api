using AutoMapper;
using GastosResidenciais.Communication.Responses.Pessoas;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Exception;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Pessoas.GetById
{
    public class GetPessoaByIdUseCase : IGetPessoaByIdUseCase
    {
        private readonly IPessoaReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        
        public GetPessoaByIdUseCase(
            IPessoaReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponsePessoaJson> Execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.PESSOA_NAO_ENCONTRADA);
            }

            return _mapper.Map<ResponsePessoaJson>(result);
        }
    }
}
