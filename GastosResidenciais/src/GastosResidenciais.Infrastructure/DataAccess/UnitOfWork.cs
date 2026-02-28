using GastosResidenciais.Domain.Repositories;

namespace GastosResidenciais.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly GastosResidenciaisDbContext _dbContext;

        public UnitOfWork(GastosResidenciaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
