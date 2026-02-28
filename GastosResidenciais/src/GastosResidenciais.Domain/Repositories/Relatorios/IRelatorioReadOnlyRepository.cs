using GastosResidenciais.Domain.DTOs;

namespace GastosResidenciais.Domain.Repositories.Relatorios
{
    public interface IRelatorioReadOnlyRepository
    {
        Task<List<PessoaTotaisDTO>> GetPessoasTotaisAsync();
        Task<List<CategoriaTotaisDTO>> GetCategoriasTotaisAsync();
    }
}
