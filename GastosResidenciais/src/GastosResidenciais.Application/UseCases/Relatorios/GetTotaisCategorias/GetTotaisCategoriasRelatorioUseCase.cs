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

        public async Task<ResponseTotaisCategoriasJson> Execute()
        {
            var categorias = await _repository.GetCategoriasTotaisAsync();

            var lista = _mapper.Map<List<ResponseTotalCategoriaJson>>(categorias);

            var totalReceitasGeral = lista.Sum(x => x.TotalReceitas);
            var totalDespesasGeral = lista.Sum(x => x.TotalDespesas);

            return new ResponseTotaisCategoriasJson
            {
                Categorias = lista,
                TotalReceitasGeral = totalReceitasGeral,
                TotalDespesasGeral = totalDespesasGeral,
                SaldoGeral = totalReceitasGeral - totalDespesasGeral
            };
        }
    }
}
