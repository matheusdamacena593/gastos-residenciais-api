namespace GastosResidenciais.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
