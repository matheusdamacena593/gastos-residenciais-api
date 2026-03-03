using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Domain.Repositories.Categorias
{
    public interface ICategoriaReadOnlyRepository
    {
        Task<PageResultDTO<Categoria>> GetAll(int page, int pageSize);
        Task<Categoria?> GetById(long id);
        Task<bool> ExistsById(long id, CancellationToken ct);
        Task<FinalidadeCategoriaEnum?> GetFinalidadeById(long id, CancellationToken ct);
        Task<long> CountAsync();
    }
}
