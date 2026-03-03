using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories.Transacoes;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    internal class TransacaoRepository : ITransacaoReadOnlyRepository, ITransacaoWriteOnlyRepository
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

        public async Task<PageResultDTO<Transacao>> GetAll(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var baseQuery = _dbContext.Transacoes.AsNoTracking();

            var totalItems = await baseQuery.CountAsync();

            var items = await baseQuery
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultDTO<Transacao>
            {
                Page = page,
                PageSize = items.Count,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }

        public async Task<Transacao?> GetById(long id)
        {
            return await _dbContext
               .Transacoes
               .AsNoTracking()
               .FirstOrDefaultAsync(pessoa => pessoa.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _dbContext.Transacoes.LongCountAsync();
        }
    }
}
