using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Enums;

namespace GastosResidenciais.Domain.Repositories.Categorias
{
    public interface ICategoriaReadOnlyRepository
    {
        Task<List<Categoria>> GetAll();
        Task<Categoria?> GetById(long id);
        Task<bool> ExistsById(long id, CancellationToken ct);
        Task<FinalidadeCategoriaEnum?> GetFinalidadeById(long id, CancellationToken ct);
    }
}
