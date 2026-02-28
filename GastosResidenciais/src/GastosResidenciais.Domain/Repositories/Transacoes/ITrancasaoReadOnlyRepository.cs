using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Transacoes
{
    public interface ITrancasaoReadOnlyRepository
    {
        Task<List<Transacao>> GetAll();
        Task<Transacao?> GetById(long id);
    }
}
