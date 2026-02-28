using AutoMapper;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Domain.Repositories.Categorias;

namespace GastosResidenciais.Application.UseCases.Categorias.GetAll
{
    public class GetAllCategoriasUseCase : IGetAllCategoriasUseCase
    {
        private readonly ICategoriaReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriasUseCase(
            ICategoriaReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseCategoriasJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseCategoriasJson
            {
                Categorias = _mapper.Map<List<ResponseCategoriaJson>>(result)
            };
        }
    }
}
