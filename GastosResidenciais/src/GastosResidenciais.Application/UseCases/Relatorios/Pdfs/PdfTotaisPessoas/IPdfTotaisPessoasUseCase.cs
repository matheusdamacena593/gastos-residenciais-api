namespace GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisPessoas
{
    public interface IPdfTotaisPessoasUseCase
    {
        Task<byte[]> Execute();
    }
}
