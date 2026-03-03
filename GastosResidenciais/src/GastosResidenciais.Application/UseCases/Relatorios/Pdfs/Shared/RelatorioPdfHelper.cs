using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.Colors;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.Fonts;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace GastosResidenciais.Application.UseCases.Relatorios.Pdfs.Shared
{
    public class RelatorioPdfHelper
    {
        public const string CURRENCY_SYMBOL = "R$";

        static RelatorioPdfHelper()
        {
            if (GlobalFontSettings.FontResolver == null)
                GlobalFontSettings.FontResolver = new RelatoriosFontResolver();
        }

        public static Document CreateDocument(string title, string author)
        {
            var document = new Document();
            document.Info.Title = title;
            document.Info.Author = author;

            var style = document.Styles["Normal"];
            style!.Font.Name = FontHelper.RALEWAY_REGULAR;

            return document;
        }

        public static Section CreatePage(Document document)
        {
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;

            return section;
        }

        public static void CreateHeaderWithProfilePhotoAndName(Section page, string titulo)
        {
            var table = page.AddTable();
            table.AddColumn();
            table.AddColumn("300");

            var row = table.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var directoryName = Path.GetDirectoryName(assembly.Location);
            var pathFile = Path.Combine(directoryName!, "Logo", "logo-md.png");

            row.Cells[0].AddImage(pathFile);

            row.Cells[1].AddParagraph(titulo);
            row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
        }

        public static void CreateTotalGeralSection(Section page, decimal totalReceita, decimal totalDespesa, decimal saldo)
        {
            var titleParagraph = page.AddParagraph("Total Geral");
            titleParagraph.Format.SpaceBefore = "12";
            titleParagraph.Format.SpaceAfter = "6";
            titleParagraph.Format.Font.Name = FontHelper.RALEWAY_BLACK;
            titleParagraph.Format.Font.Size = 14;
            titleParagraph.Format.Font.Bold = true;
            titleParagraph.Format.Font.Color = ColorsHelper.BLACK;

            var table = CreateTotalsTable(page);
            AddReceitaDespesaSaldo(table, totalReceita, totalDespesa, saldo);

            var spacer = page.AddParagraph();
            spacer.Format.SpaceAfter = "10";
        }

        public static Table CreateTotalsTable(Section page)
        {
            var table = page.AddTable();
            table.Format.Font.Name = FontHelper.WORKSANS_REGULAR;
            table.Format.Font.Size = 11;
            table.Format.Font.Color = ColorsHelper.BLACK;

            table.Borders.Width = 0.6;
            table.Borders.Color = ColorsHelper.GREEN_DARK;

            var usableWidth = page.PageSetup.PageWidth - page.PageSetup.LeftMargin - page.PageSetup.RightMargin;
            var colWidth = usableWidth / 3;

            table.AddColumn(colWidth);
            table.AddColumn(colWidth);
            table.AddColumn(colWidth);

            return table;
        }

        public static void AddDescricaoCategoria(Cells cells, string descricao)
        {
            cells[0].AddParagraph(descricao);
            cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
            cells[0].Shading.Color = ColorsHelper.RED_LIGTH;
            cells[0].VerticalAlignment = VerticalAlignment.Center;
            cells[0].MergeRight = 2;
            cells[0].Format.LeftIndent = 20;
        }

        public static void AddNomeAndIdadePessoa(Cells cells, string nome, int idade)
        {
            cells[0].AddParagraph(nome);
            cells[0].Format.Font = new Font
            {
                Name = FontHelper.RALEWAY_BLACK,
                Size = 14,
                Color = ColorsHelper.BLACK
            };
            cells[0].Shading.Color = ColorsHelper.RED_LIGTH;
            cells[0].VerticalAlignment = VerticalAlignment.Center;
            cells[0].MergeRight = 1;
            cells[0].Format.LeftIndent = 20;

            cells[2].AddParagraph($"{idade} anos");
            cells[2].Format.Font = new Font
            {
                Name = FontHelper.RALEWAY_REGULAR,
                Size = 14,
                Color = ColorsHelper.BLACK
            };
            cells[2].Shading.Color = ColorsHelper.RED_LIGTH;
            cells[2].VerticalAlignment = VerticalAlignment.Center;
            cells[2].Format.LeftIndent = 20;
        }

        public static void AddReceitaDespesaSaldo(Table table, decimal totalReceita, decimal totalDespesa, decimal saldo)
        {
            var header = table.AddRow();
            header.Height = "0.65cm";
            header.Shading.Color = ColorsHelper.GREEN_DARK;
            header.Format.Alignment = ParagraphAlignment.Center;
            header.VerticalAlignment = VerticalAlignment.Center;
            header.Format.Font.Bold = true;
            header.Format.Font.Color = ColorsHelper.BLACK;

            header.Cells[0].AddParagraph("Receita");
            header.Cells[1].AddParagraph("Despesa");
            header.Cells[2].AddParagraph("Saldo");

            var values = table.AddRow();
            values.Height = "0.75cm";
            values.Shading.Color = ColorsHelper.GREEN_LIGTH;
            values.Format.Alignment = ParagraphAlignment.Center;
            values.VerticalAlignment = VerticalAlignment.Center;
            values.Format.Font.Bold = false;
            values.Format.Font.Color = ColorsHelper.BLACK;

            values.Cells[0].AddParagraph($"{CURRENCY_SYMBOL} {totalReceita:N2}");
            values.Cells[1].AddParagraph($"{CURRENCY_SYMBOL} {totalDespesa:N2}");
            values.Cells[2].AddParagraph($"{CURRENCY_SYMBOL} {saldo:N2}");

            if (saldo < 0)
            {
                values.Cells[2].Shading.Color = ColorsHelper.RED_LIGTH;
                values.Cells[2].Format.Font.Color = ColorsHelper.RED_DARK;
                values.Cells[2].Format.Font.Bold = true;
            }
        }

        public static void AddWhiteSpaces(Table table)
        {
            var row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }

        public static byte[] RenderDocument(Document document)
        {
            var renderer = new PdfDocumentRenderer
            {
                Document = document,
            };

            renderer.RenderDocument();

            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);

            return file.ToArray();
        }
    }
}
