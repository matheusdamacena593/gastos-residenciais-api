using AutoMapper;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.Repositories.Categorias;
using GastosResidenciais.Domain.Repositories.Transacoes;
using GastosResidenciais.Exception;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Transacoes.GetById
{
    public class GetTransacaoByIdUseCase : IGetTransacaoByIdUseCase
    {
        private readonly ITrancasaoReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetTransacaoByIdUseCase(
            ITrancasaoReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseTransacaoJson> Execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRANSACAO_NAO_ENCONTRADA);
            }

            return _mapper.Map<ResponseTransacaoJson>(result);
        }
    }
}
