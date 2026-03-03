using GastosResidenciais.Domain.DTOs;

namespace GastosResidenciais.Domain.Repositories.Relatorios
{
    public interface IRelatorioReadOnlyRepository
    {
        Task<List<PessoaTotaisDTO>> GetPessoasTotaisAsync();
        Task<PageResultDTO<PessoaTotaisDTO>> GetPessoasTotaisPaginadoAsync(int page, int pageSize);
        Task<TotaisGeraisDTO> GetTotaisGeraisPessoasAsync();
        Task<List<CategoriaTotaisDTO>> GetCategoriasTotaisAsync();
        Task<PageResultDTO<CategoriaTotaisDTO>> GetCategoriasTotaisPaginadoAsync(int page, int pageSize);
        Task<TotaisGeraisDTO> GetTotaisGeraisCategoriasAsync();
    }
}
