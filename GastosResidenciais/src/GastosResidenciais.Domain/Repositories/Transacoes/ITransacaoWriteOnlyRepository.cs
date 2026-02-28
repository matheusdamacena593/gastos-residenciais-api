using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Transacoes
{
    public interface ITransacaoWriteOnlyRepository
    {
        Task Add(Transacao transacao);
    }
}
