using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories.Transacoes;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    internal class TransacaoRepository : ITrancasaoReadOnlyRepository, ITransacaoWriteOnlyRepository
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        public TransacaoRepository(GastosResidenciaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Transacao transacao)
        {
            await _dbContext
                .Transacoes
                .AddAsync(transacao);
        }

        public async Task<List<Transacao>> GetAll()
        {
            return await _dbContext
                .Transacoes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Transacao?> GetById(long id)
        {
            return await _dbContext
               .Transacoes
               .AsNoTracking()
               .FirstOrDefaultAsync(pessoa => pessoa.Id == id);
        }
    }
}
