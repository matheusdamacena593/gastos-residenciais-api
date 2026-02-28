using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    internal class PessoaRepository : IPessoaReadOnlyRepository, IPessoaWriteOnlyRepository, IPessoaUpdateOnlyRepository
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        public PessoaRepository(GastosResidenciaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Pessoa pessoa)
        {
            await _dbContext
                .Pessoas
                .AddAsync(pessoa);
        }

        public async Task<bool> Delete(long id)
        {
            var result = await _dbContext
                .Pessoas
                .FirstOrDefaultAsync(pessoa => pessoa.Id == id);

            if (result is null)
            {
                return false;
            }

            _dbContext
                .Pessoas
                .Remove(result);

            return true;
        }

        public async Task<List<Pessoa>> GetAll()
        {
            return await _dbContext
                .Pessoas
                .AsNoTracking()
                .ToListAsync();
        }

        async Task<Pessoa?> IPessoaReadOnlyRepository.GetById(long id)
        {
            return await _dbContext
                .Pessoas
                .AsNoTracking()
                .FirstOrDefaultAsync(pessoa => pessoa.Id == id);
        }

        async Task<Pessoa?> IPessoaUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext
                .Pessoas
                .FirstOrDefaultAsync(pessoa => pessoa.Id == id);
        }

        public void Update(Pessoa pessoa)
        {
            _dbContext
                .Pessoas
                .Update(pessoa);
        }

        public Task<bool> ExistsById(long id, CancellationToken ct)
        {
            return _dbContext.Pessoas.AsNoTracking().AnyAsync(c => c.Id == id, ct);
        }
    }
}
