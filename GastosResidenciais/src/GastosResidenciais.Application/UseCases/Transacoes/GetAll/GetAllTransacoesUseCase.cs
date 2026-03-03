using AutoMapper;
using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Repositories.Transacoes;

namespace GastosResidenciais.Application.UseCases.Transacoes.GetAll
{
    public class GetAllTransacoesUseCase : IGetAllTransacoesUseCase
    {
        private readonly ITransacaoReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTransacoesUseCase(
            ITransacaoReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageResultDTO<ResponseTransacaoJson>> Execute(int page, int pageSize)
        {
            var result = await _repository.GetAll(page, pageSize);

            return new PageResultDTO<ResponseTransacaoJson>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Items = _mapper.Map<List<ResponseTransacaoJson>>(result.Items)
            };
        }
    }
}
