using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Pessoas
{
    public interface IPessoaReadOnlyRepository
    {
        Task<PageResultDTO<Pessoa>> GetAll(int page, int pageSize);
        Task<Pessoa?> GetById(long id);
        Task<bool> ExistsById(long id, CancellationToken ct);
        Task<long> CountAsync();
    }
}
