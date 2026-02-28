using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Pessoas
{
    public interface IPessoaReadOnlyRepository
    {
        Task<List<Pessoa>> GetAll();
        Task<Pessoa?> GetById(long id);
        Task<bool> ExistsById(long id, CancellationToken ct);
    }
}
