using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Transacoes
{
    public interface ITransacaoReadOnlyRepository
    {
        Task<PageResultDTO<Transacao>> GetAll(int page, int pageSize);
        Task<Transacao?> GetById(long id);
        Task<long> CountAsync();
    }
}
