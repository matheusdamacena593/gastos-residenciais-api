using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Pessoas
{
    public interface IPessoaWriteOnlyRepository
    {
        Task Add(Pessoa pessoa);
        Task<bool> Delete(long id);
    }
}
