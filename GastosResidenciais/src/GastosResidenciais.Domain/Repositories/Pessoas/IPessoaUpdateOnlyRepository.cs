using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Pessoas
{
    public interface IPessoaUpdateOnlyRepository
    {
        Task<Pessoa?> GetById(long id);
        void Update(Pessoa pessoa);
    }
}
