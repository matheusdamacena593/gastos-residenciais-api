namespace GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisCategorias
{
    public interface IPdfTotaisCategoriasUseCase
    {
        Task<byte[]> Execute();
    }
}
