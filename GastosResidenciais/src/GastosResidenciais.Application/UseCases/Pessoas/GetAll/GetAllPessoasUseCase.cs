using AutoMapper;
using GastosResidenciais.Communication.Responses.Pessoas;
using GastosResidenciais.Domain.DTOs;
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

        public async Task<PageResultDTO<ResponsePessoaJson>> Execute(int page, int pageSize)
        {
            var result = await _repository.GetAll(page, pageSize);

            return new PageResultDTO<ResponsePessoaJson>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Items = _mapper.Map<List<ResponsePessoaJson>>(result.Items)
            };
        }
    }
}
