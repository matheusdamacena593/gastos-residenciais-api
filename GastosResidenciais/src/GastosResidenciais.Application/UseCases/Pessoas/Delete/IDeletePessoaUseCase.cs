namespace GastosResidenciais.Application.UseCases.Pessoas.Delete
{
    public interface IDeletePessoaUseCase
    {
        Task Execute(long id);
    }
}
