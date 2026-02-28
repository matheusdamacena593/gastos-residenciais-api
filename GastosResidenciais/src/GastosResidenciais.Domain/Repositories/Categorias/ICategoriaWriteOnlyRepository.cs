using GastosResidenciais.Domain.Entities;

namespace GastosResidenciais.Domain.Repositories.Categorias
{
    public interface ICategoriaWriteOnlyRepository
    {
        Task Add(Categoria categoria);
    }
}
