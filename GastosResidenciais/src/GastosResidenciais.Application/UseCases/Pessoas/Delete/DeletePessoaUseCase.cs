using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Exception;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Pessoas.Delete
{
    public class DeletePessoaUseCase : IDeletePessoaUseCase
    {
        private readonly IPessoaWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePessoaUseCase(
            IPessoaWriteOnlyRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);

            if (result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.PESSOA_NAO_ENCONTRADA);
            }

            await _unitOfWork.Commit();
        }
    }
}
