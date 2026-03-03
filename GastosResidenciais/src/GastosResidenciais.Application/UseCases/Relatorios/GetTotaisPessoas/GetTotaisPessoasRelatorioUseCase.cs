using AutoMapper;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Domain.Repositories.Relatorios;

namespace GastosResidenciais.Application.UseCases.Relatorios.GetTotaisPessoas
{
    public class GetTotaisPessoasRelatorioUseCase : IGetTotaisPessoasRelatorioUseCase
    {
        private readonly IRelatorioReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetTotaisPessoasRelatorioUseCase(
            IRelatorioReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseTotaisPessoasJson> Execute(int page, int pageSize)
        {
            var pageResult = await _repository.GetPessoasTotaisPaginadoAsync(page, pageSize);
            var totaisGerais = await _repository.GetTotaisGeraisPessoasAsync();

            var listaJson = _mapper.Map<List<ResponseTotalPessoaJson>>(pageResult.Items);

            var pessoasPageResponse = new Communication.DTOs.PageResultDTO<ResponseTotalPessoaJson>
            {
                Page = pageResult.Page,
                PageSize = pageResult.PageSize,
                TotalItems = pageResult.TotalItems,
                TotalPages = pageResult.TotalPages,
                Items = listaJson
            };

            return new ResponseTotaisPessoasJson
            {
                Pessoas = pessoasPageResponse,
                TotalReceitasGeral = totaisGerais.TotalReceitasGeral,
                TotalDespesasGeral = totaisGerais.TotalDespesasGeral,
                SaldoGeral = totaisGerais.TotalReceitasGeral - totaisGerais.TotalDespesasGeral
            };
        }
    }
}
