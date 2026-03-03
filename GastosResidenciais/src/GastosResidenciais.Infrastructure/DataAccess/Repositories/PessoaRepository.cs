using GastosResidenciais.Domain.DTOs;
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

        public async Task<PageResultDTO<Pessoa>> GetAll(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var baseQuery = _dbContext.Pessoas.AsNoTracking();

            var totalItems = await baseQuery.CountAsync();

            var items = await baseQuery
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultDTO<Pessoa>
            {
                Page = page,
                PageSize = items.Count,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
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

        public async Task<long> CountAsync()
        {
            return await _dbContext.Pessoas.LongCountAsync();
        }
    }
}
