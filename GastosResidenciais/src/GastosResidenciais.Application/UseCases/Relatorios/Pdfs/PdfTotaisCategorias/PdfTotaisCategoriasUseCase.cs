using AutoMapper;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.Shared;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Domain.Repositories.Relatorios;
using MigraDoc.DocumentObjectModel;

namespace GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisCategorias
{
    public class PdfTotaisCategoriasUseCase : IPdfTotaisCategoriasUseCase
    {
        private const int HEIGHT_ROW_EXPENSE_TABLE = 25;

        private readonly IRelatorioReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public PdfTotaisCategoriasUseCase(
            IRelatorioReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<byte[]> Execute()
        {
            var categorias = await _repository.GetCategoriasTotaisAsync();

            if (categorias.Count == 0)
                return [];

            var lista = _mapper.Map<List<ResponseTotalCategoriaJson>>(categorias);

            var totalReceitasGeral = lista.Sum(x => x.TotalReceitas);
            var totalDespesasGeral = lista.Sum(x => x.TotalDespesas);
            var saldoGeral = totalReceitasGeral - totalDespesasGeral;

            var document = RelatorioPdfHelper.CreateDocument(
                title: "Gastos Totais Por Categoria",
                author: "Matheus Damacena"
            );

            var page = RelatorioPdfHelper.CreatePage(document);

            RelatorioPdfHelper.CreateHeaderWithProfilePhotoAndName(page, "Gastos totais por categoria");
            RelatorioPdfHelper.CreateTotalGeralSection(page, totalReceitasGeral, totalDespesasGeral, saldoGeral);

            foreach (var categoria in lista)
            {
                var table = RelatorioPdfHelper.CreateTotalsTable(page);

                var row = table.AddRow();
                row.Height = Unit.FromPoint(HEIGHT_ROW_EXPENSE_TABLE);

                RelatorioPdfHelper.AddDescricaoCategoria(row.Cells, categoria.Descricao);

                RelatorioPdfHelper.AddReceitaDespesaSaldo(
                    table,
                    categoria.TotalReceitas,
                    categoria.TotalDespesas,
                    categoria.Saldo
                );

                RelatorioPdfHelper.AddWhiteSpaces(table);
            }

            return RelatorioPdfHelper.RenderDocument(document);
        }
    }
}
