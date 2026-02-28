using AutoMapper;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Domain.Repositories.Categorias;
using GastosResidenciais.Exception;
using GastosResidenciais.Exception.ExceptionsBase;

namespace GastosResidenciais.Application.UseCases.Categorias.GetById
{
    public class GetCategoriaByIdUseCase : IGetCategoriaByIdUseCase
    {
        private readonly ICategoriaReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoriaByIdUseCase(
            ICategoriaReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseCategoriaJson> Execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.CATEGORIA_NAO_ENCONTRADA);
            }

            return _mapper.Map<ResponseCategoriaJson>(result);
        }
    }
}
