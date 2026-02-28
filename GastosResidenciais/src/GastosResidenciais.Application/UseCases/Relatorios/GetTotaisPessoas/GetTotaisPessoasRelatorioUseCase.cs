using AutoMapper;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Domain.Repositories.Pessoas;
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

        public async Task<ResponseTotaisPessoasJson> Execute()
        {
            var pessoas = await _repository.GetPessoasTotaisAsync();

            var lista = _mapper.Map<List<ResponseTotalPessoaJson>>(pessoas);

            var totalReceitasGeral = lista.Sum(x => x.TotalReceitas);
            var totalDespesasGeral = lista.Sum(x => x.TotalDespesas);

            return new ResponseTotaisPessoasJson
            {
                Pessoas = lista,
                TotalReceitasGeral = totalReceitasGeral,
                TotalDespesasGeral = totalDespesasGeral,
                SaldoGeral = totalReceitasGeral - totalDespesasGeral
            };
        }
    }
}
