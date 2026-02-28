using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Enums;
using GastosResidenciais.Domain.Repositories.Categorias;
using Microsoft.EntityFrameworkCore;

namespace GastosResidenciais.Infrastructure.DataAccess.Repositories
{
    internal class CategoriaRepository : ICategoriaReadOnlyRepository, ICategoriaWriteOnlyRepository
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        public CategoriaRepository(GastosResidenciaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Categoria categoria)
        {
            await _dbContext
                .Categorias
                .AddAsync(categoria);
        }

        public async Task<bool> Delete(long id)
        {
            var result = await _dbContext
                .Categorias
                .FirstOrDefaultAsync(categoria => categoria.Id == id);

            if (result is null)
            {
                return false;
            }

            _dbContext
                .Categorias
                .Remove(result);

            return true;
        }

        public async Task<List<Categoria>> GetAll()
        {
            return await _dbContext
                .Categorias
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categoria?> GetById(long id)
        {
            return await _dbContext
               .Categorias
               .AsNoTracking()
               .FirstOrDefaultAsync(categoria => categoria.Id == id);
        }

        public Task<bool> ExistsById(long id, CancellationToken ct)
        => _dbContext.Categorias.AsNoTracking().AnyAsync(c => c.Id == id, ct);

        public async Task<FinalidadeCategoriaEnum?> GetFinalidadeById(long id, CancellationToken ct)
        {
            return await _dbContext.Categorias.AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => (FinalidadeCategoriaEnum?)c.Finalidade)
                .FirstOrDefaultAsync(ct);
        }
    }
}
