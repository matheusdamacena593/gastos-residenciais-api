using AutoMapper;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Domain.Entities;
using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Categorias;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Categorias.Register
{
    public class RegisterCategoriaUseCase : IRegisterCategoriaUseCase
    {
        private readonly ICategoriaWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCategoriaUseCase(
            ICategoriaWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseCategoriaJson> Execute(RequestCategoriaJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Categoria>(request);

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseCategoriaJson>(entity);
        }

        private void Validate(RequestCategoriaJson request)
        {
            var validator = new CategoriaValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
