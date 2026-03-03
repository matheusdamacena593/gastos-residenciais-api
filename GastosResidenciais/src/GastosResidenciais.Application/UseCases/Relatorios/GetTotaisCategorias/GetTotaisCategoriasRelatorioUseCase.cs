using AutoMapper;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Domain.Repositories.Relatorios;

namespace GastosResidenciais.Application.UseCases.Relatorios.GetTotaisCategorias
{
    public class GetTotaisCategoriasRelatorioUseCase : IGetTotaisCategoriasRelatorioUseCase
    {
        private readonly IRelatorioReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetTotaisCategoriasRelatorioUseCase(IRelatorioReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseTotaisCategoriasJson> Execute(int page, int pageSize)
        {
            var pageResult = await _repository.GetCategoriasTotaisPaginadoAsync(page, pageSize);
            var totaisGerais = await _repository.GetTotaisGeraisCategoriasAsync();

            var listaJson = _mapper.Map<List<ResponseTotalCategoriaJson>>(pageResult.Items);

            var categoriasPageResponse = new Communication.DTOs.PageResultDTO<ResponseTotalCategoriaJson>
            {
                Page = pageResult.Page,
                PageSize = pageResult.PageSize,
                TotalItems = pageResult.TotalItems,
                TotalPages = pageResult.TotalPages,
                Items = listaJson
            };

            return new ResponseTotaisCategoriasJson
            {
                Categorias = categoriasPageResponse,
                TotalReceitasGeral = totaisGerais.TotalReceitasGeral,
                TotalDespesasGeral = totaisGerais.TotalDespesasGeral,
                SaldoGeral = totaisGerais.TotalReceitasGeral - totaisGerais.TotalDespesasGeral
            };
        }
    }
}
