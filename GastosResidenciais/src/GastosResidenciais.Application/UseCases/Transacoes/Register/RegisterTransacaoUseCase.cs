using AutoMapper;
using FluentValidation;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Transacoes;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Transacoes.Register
{
    public class RegisterTransacaoUseCase : IRegisterTransacaoUseCase
    {
        private readonly ITransacaoWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequestTransacaoJson> _validator;

        public RegisterTransacaoUseCase(
            ITransacaoWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<RequestTransacaoJson> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResponseTransacaoJson> Execute(RequestTransacaoJson request)
        {
            await ValidateAsync(request);

            var entity = _mapper.Map<Transacao>(request);

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseTransacaoJson>(entity);
        }

        private async Task ValidateAsync(RequestTransacaoJson request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
