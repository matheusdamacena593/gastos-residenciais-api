using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Pessoas;
using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Exception.ExceptionsBase;
using PdfSharp.Drawing;

namespace GastosResidenciais.Application.UseCases.Pessoas.Register
{
    public class RegisterPessoaUseCase : IRegisterPessoaUseCase
    {
        private readonly IPessoaWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterPessoaUseCase(
            IPessoaWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePessoaJson> Execute(RequestPessoaJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Pessoa>(request);

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePessoaJson>(entity);
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
