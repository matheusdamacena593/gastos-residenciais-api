using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Exception;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Pessoas.Update
{
    public class UpdatePessoaUseCase : IUpdatePessoaUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPessoaUpdateOnlyRepository _repository;

        public UpdatePessoaUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPessoaUpdateOnlyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Execute(long id, RequestPessoaJson request)
        {
            Validate(request);

            var pessoa = await _repository.GetById(id);

            if (pessoa is null)
            {
                throw new NotFoundException(ResourceErrorMessages.PESSOA_NAO_ENCONTRADA);
            }

            _mapper.Map(request, pessoa);

            pessoa.UpdatedAt = DateTime.UtcNow;

            _repository.Update(pessoa);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestPessoaJson request)
        {
            var validator = new PessoaValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
