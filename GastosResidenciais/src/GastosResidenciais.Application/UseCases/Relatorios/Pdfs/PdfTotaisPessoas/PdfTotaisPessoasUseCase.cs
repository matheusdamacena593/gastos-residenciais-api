using AutoMapper;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.Shared;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Domain.Repositories.Relatorios;
using MigraDoc.DocumentObjectModel;

namespace GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisPessoas
{
    public class PdfTotaisPessoasUseCase : IPdfTotaisPessoasUseCase
    {
        private const int HEIGHT_ROW_EXPENSE_TABLE = 25;

        private readonly IRelatorioReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public PdfTotaisPessoasUseCase(
            IRelatorioReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<byte[]> Execute()
        {
            var pessoas = await _repository.GetPessoasTotaisAsync();

            if (pessoas.Count == 0)
                return [];

            var lista = _mapper.Map<List<ResponseTotalPessoaJson>>(pessoas);

            var totalReceitasGeral = lista.Sum(x => x.TotalReceitas);
            var totalDespesasGeral = lista.Sum(x => x.TotalDespesas);
            var saldoGeral = totalReceitasGeral - totalDespesasGeral;

            var document = RelatorioPdfHelper.CreateDocument(
                title: "Gastos Totais Por Pessoa",
                author: "Matheus Damacena"
            );

            var page = RelatorioPdfHelper.CreatePage(document);

            RelatorioPdfHelper.CreateHeaderWithProfilePhotoAndName(page, "Gastos totais por pessoa");
            RelatorioPdfHelper.CreateTotalGeralSection(page, totalReceitasGeral, totalDespesasGeral, saldoGeral);

            foreach (var pessoa in lista)
            {
                var table = RelatorioPdfHelper.CreateTotalsTable(page);

                var row = table.AddRow();
                row.Height = Unit.FromPoint(HEIGHT_ROW_EXPENSE_TABLE);

                RelatorioPdfHelper.AddNomeAndIdadePessoa(row.Cells, pessoa.Nome, pessoa.Idade);

                RelatorioPdfHelper.AddReceitaDespesaSaldo(
                    table,
                    pessoa.TotalReceitas,
                    pessoa.TotalDespesas,
                    pessoa.Saldo
                );

                RelatorioPdfHelper.AddWhiteSpaces(table);
            }

            return RelatorioPdfHelper.RenderDocument(document);
        }
    }
}
